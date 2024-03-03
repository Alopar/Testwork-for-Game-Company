using UnityEngine;
using Infrastructure.Factory;

namespace Services.ScreenSystem
{
    public class ScreenViewFactory : AbstractFactory
    {
        #region METHODS PUBLIC
        public AbstractView Create(AbstractView prefab)
        {
            var tempState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);

            var instance = GameObject.Instantiate(prefab);
            var children = instance.GetComponentsInChildren<MonoBehaviour>(true);
            foreach (var child in children)
            {
                _resolver.Resolve(child);
                _container.Inject(child);
            }

            instance.gameObject.SetActive(tempState);
            prefab.gameObject.SetActive(tempState);

            return instance;
        }
        #endregion
    }
}
