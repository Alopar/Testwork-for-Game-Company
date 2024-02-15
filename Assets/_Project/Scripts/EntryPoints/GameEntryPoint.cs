using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using Services.SceneLoader;
using Services.ScreenSystem;
using Services.PurchaseSystem;
using Utility.CoroutineRunner;
using Utility.DependencyInjection;
using Screens.Layers.Purchase;
using Container = Utility.DependencyInjection.DependencyContainer;

namespace EntryPoint
{
    public static class GameEntryPoint
    {
        #region FIELDS PRIVATE
        private const string SCREENS_ASSET_LABEL = "screens";
        private const string PURCHASE_ASSET_LABEL = "purchases";
        private static int _startSceneIndex = 0;
        #endregion

        #region PROPERTIES
        public static int StartSceneIndex => _startSceneIndex;
        #endregion

        #region METHODS PUBLIC
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void GameInitialize()
        {
            RegisterDependencyContext();
            LoadBootstrapScene();
        }
        #endregion

        #region METHODS PRIVATE
        private static void RegisterDependencyContext()
        {
            BindCoroutineRunner();
            Container.Bind<ComponentResolver>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoader>().AsSingle();

            BindScreenService();
            BindPurchaseService();
        }

        private static void BindCoroutineRunner()
        {
            var gameObject = new GameObject("[CoroutineRunner]");
            var coroutineRunner = gameObject.AddComponent<CoroutineRunner>();
            GameObject.DontDestroyOnLoad(coroutineRunner);

            Container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner);
        }

        private static void BindScreenService()
        {
            var screenPrefabs = new List<AbstractView>();
            Addressables.LoadAssetsAsync<GameObject>(SCREENS_ASSET_LABEL, (asset) => {
                if (asset.TryGetComponent<AbstractView>(out var screen))
                {
                    screenPrefabs.Add(screen);
                }
            }).WaitForCompletion();

            Container.Bind<ScreenViewFactory>();
            Container.Bind<ScreenPresenterFactory>();
            Container.Bind<IScreenService>().FromInstance(new ScreenSystem(screenPrefabs));
        }

        private static void BindPurchaseService()
        {
            var purchaseSO = new List<PurchaseCard>();
            Addressables.LoadAssetsAsync<ScriptableObject>(PURCHASE_ASSET_LABEL, (asset) => {
                purchaseSO.Add(asset as PurchaseCard);
            }).WaitForCompletion();

            Container.Bind<IPurchaseService>().FromInstance(new PurchaseSystem(purchaseSO));
        }

        private static void LoadBootstrapScene()
        {
            var scene = SceneManager.GetActiveScene();
            if (scene.buildIndex == 0) return;

            _startSceneIndex = scene.buildIndex;
            SceneManager.LoadScene(0);
        }
        #endregion
    }
}
