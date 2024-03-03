using UnityEngine;
using UnityEngine.AddressableAssets;
using Services.SceneLoader;
using Utility.DependencyInjection;

namespace EntryPoint
{
    [DefaultExecutionOrder(-100)]
    public class GameBootstrapper : AbstractBootstrapper
    {
        #region FIELDS PRIVATE
        private const string SYSTEMS_ASSET_LABEL = "systems";

        [Inject] private ISceneLoaderService _sceneLoaderService;
        #endregion

        #region METHODS PRIVATE
        protected override void InitializeScene()
        {
            InitializeSystems();
            LoadGameScene();
        }

        private void InitializeSystems()
        {
            var systems = new GameObject("[Systems]");
            Addressables.LoadAssetsAsync<GameObject>(SYSTEMS_ASSET_LABEL, (asset) => {
                var system = Instantiate(asset);
                system.name = asset.name;
                system.transform.SetParent(systems.transform);
            }).WaitForCompletion();

            DontDestroyOnLoad(systems);
        }

        private void LoadGameScene()
        {
            var sceneIndex = GameEntryPoint.StartSceneIndex == 0 ? 1 : GameEntryPoint.StartSceneIndex;
            _sceneLoaderService.Load(sceneIndex);
        }
        #endregion
    }
}
