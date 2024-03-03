using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Utility.DependencyInjection;

namespace EntryPoint
{
    [DefaultExecutionOrder(-100)]
    public abstract class AbstractBootstrapper : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private List<MonoBehaviour> _dependants;
        #endregion

        #region FIELDS PRIVATE
        private static DependencyContainer _container;
        private static ComponentResolver _resolver;
        #endregion

        #region METHODS PUBLIC
        public static void Initialization(DependencyContainer container, ComponentResolver resolver)
        {
            _container = container;
            _resolver = resolver;
        }

        [ContextMenu("FIND DEPENDANTS")]
        public void FindDependants()
        {
            _dependants = FindObjectsOfType<MonoBehaviour>().Where(e => e is IDependant).ToList();
        }
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            SelfResolve();
            ResolveDependency();
        }

        private void Start()
        {
            InitializeScene();
        }
        #endregion

        #region METHODS PRIVATE
        private void SelfResolve()
        {
            _container.Inject(this);
        }

        private void ResolveDependency()
        {
            foreach (var dependant in _dependants)
            {
                var children = dependant.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (var child in children)
                {
                    _container.Inject(child);
                    _resolver.Resolve(child);
                }
            }
        }

        protected abstract void InitializeScene();
        #endregion
    }
}
