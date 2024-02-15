using UnityEngine;
using Utility.DependencyInjection;

namespace Services.ScreenSystem
{
    public class ScreenViewFactory
    {
        #region FIELDS PRIVATE
        [Inject] private ComponentResolver _componentResolver;
        #endregion

        #region METHODS PUBLIC
        public AbstractView Create(AbstractView prefab)
        {
            var tempState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);

            var instance = GameObject.Instantiate(prefab);
            var children = instance.GetComponentsInChildren<MonoBehaviour>(true);
            foreach (var child in children)
            {
                _componentResolver.Resolve(child);
                DependencyContainer.Inject(child);
            }

            instance.gameObject.SetActive(tempState);
            prefab.gameObject.SetActive(tempState);

            return instance;
        }
        #endregion
    }
}
