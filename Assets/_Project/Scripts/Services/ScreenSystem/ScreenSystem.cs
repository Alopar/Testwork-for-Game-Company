using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Utility.DependencyInjection;

namespace Services.ScreenSystem
{
    public class ScreenSystem : IScreenService
    {
        #region FIELDS PRIVATE
        [Inject] private readonly ScreenViewFactory _viewFactory;
        [Inject] private readonly ScreenPresenterFactory _presenterFactory;
        private readonly List<AbstractView> _viewPrefabs;

        private Transform _holder;
        private Dictionary<ScreenType, IScreenPresenter> _presenters = new Dictionary<ScreenType, IScreenPresenter>();
        #endregion

        #region EVENTS
        public event Action<ScreenType> OnScreenOpen;
        public event Action<ScreenType> OnScreenClose;
        #endregion

        #region CONSTRUCTORS
        public ScreenSystem(List<AbstractView> viewPrefabs)
        {
            _viewPrefabs = viewPrefabs;
        }
        #endregion

        #region METHODS PUBLIC
        public void InitializeScreens(ScreenType[] screenTypes)
        {
            InitializeHolder();
            foreach (var screenType in screenTypes)
            {
                var prefab = _viewPrefabs.FirstOrDefault(e => e.ScreenType == screenType);
                if (prefab == null)
                {
                    Debug.LogError($"Screen prefab with type {screenType} not found!");
                    continue;
                }

                var view = _viewFactory.Create(prefab);
                view.transform.parent = _holder;
                view.name = prefab.name;
                view.HideScreen();

                var presenter = _presenterFactory.Create(screenType, view);
                _presenters.Add(screenType, presenter);
            }
        }

        public void OpenScreen(ScreenType type, object payload = null)
        {
            if (!CheckAvailableScreen(type)) return;

            _presenters[type].OpenScreen(payload);
            OnScreenOpen?.Invoke(type);
        }

        public void CloseScreen(ScreenType type)
        {
            if (!CheckAvailableScreen(type)) return;

            _presenters[type].CloseScreen();
            OnScreenClose?.Invoke(type);
        }

        public void ClearScreens()
        {
            foreach (var screen in _presenters.Values)
            {
                screen.DestroyView();
            }

            _presenters.Clear();
        }
        #endregion

        #region METHODS PRIVATE
        private void InitializeHolder()
        {
            if (_holder != null) return;

            _holder = new GameObject("[Screens]").transform;
            _holder.position = new Vector3(0, 0, 100);
            GameObject.DontDestroyOnLoad(_holder);
        }

        private bool CheckAvailableScreen(ScreenType type)
        {
            if (!_presenters.ContainsKey(type))
            {
                Debug.LogError($"Screen with type {type} not initialized!");
                return false;
            }

            return true;
        }
        #endregion
    }
}
