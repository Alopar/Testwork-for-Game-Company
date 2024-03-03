using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using Services.SceneLoader;
using Services.ScreenSystem;
using Services.PurchaseSystem;
using Infrastructure.Factory;
using Utility.CoroutineRunner;
using Utility.DependencyInjection;
using Screens.Layers.Purchase;
using DIC = Utility.DependencyInjection.DependencyContainer;

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
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void GameInitialize()
        {
            RegisterDependencyContext();
            LoadBootstrapScene();
        }
        #endregion

        #region METHODS PRIVATE
        private static void RegisterDependencyContext()
        {
            var container = CreateDependencySystem();

            BindCoroutineRunner(container);

            container.Bind<ISceneLoaderService>().To<SceneLoader>().AsSingle();

            BindScreenService(container);
            BindPurchaseService(container);
        }

        private static DIC CreateDependencySystem()
        {
            var container = new DIC();
            var resolver = new ComponentResolver();

            AbstractDependency.Initialization(container);
            AbstractFactory.Initialization(container, resolver);
            AbstractBootstrapper.Initialization(container, resolver);

            return container;
        }

        private static void BindCoroutineRunner(DIC container)
        {
            var gameObject = new GameObject("[CoroutineRunner]");
            var coroutineRunner = gameObject.AddComponent<CoroutineRunner>();
            GameObject.DontDestroyOnLoad(coroutineRunner);

            container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner);
        }

        private static void BindScreenService(DIC container)
        {
            var screenPrefabs = new List<AbstractView>();
            Addressables.LoadAssetsAsync<GameObject>(SCREENS_ASSET_LABEL, (asset) => {
                if (asset.TryGetComponent<AbstractView>(out var screen))
                {
                    screenPrefabs.Add(screen);
                }
            }).WaitForCompletion();

            container.Bind<ScreenViewFactory>();
            container.Bind<ScreenPresenterFactory>();
            container.Bind<IScreenService>().FromInstance(new ScreenSystem(screenPrefabs));
        }

        private static void BindPurchaseService(DIC container)
        {
            var purchaseSO = new List<PurchaseCard>();
            Addressables.LoadAssetsAsync<ScriptableObject>(PURCHASE_ASSET_LABEL, (asset) => {
                purchaseSO.Add(asset as PurchaseCard);
            }).WaitForCompletion();

            container.Bind<IPurchaseService>().FromInstance(new PurchaseSystem(purchaseSO));
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
