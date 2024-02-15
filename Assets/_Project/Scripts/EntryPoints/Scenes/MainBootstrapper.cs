using UnityEngine;
using Services.ScreenSystem;
using Utility.DependencyInjection;

namespace EntryPoint
{
    [DefaultExecutionOrder(-100)]
    public class MainBootstrapper : AbstractBootstrapper
    {
        #region FIELDS PRIVATE
        [Inject] private IScreenService _screenService;
        #endregion

        #region METHODS PRIVATE
        protected override void InitializeScene()
        {
            InitializeScreens();
            ShowScreens();
        }

        private void InitializeScreens()
        {
            var screenTypes = new ScreenType[] {
                ScreenType.MainMenuLayer,
                ScreenType.PurchaseModal,

            };
            _screenService.ClearScreens();
            _screenService.InitializeScreens(screenTypes);
        }

        private void ShowScreens()
        {
            _screenService.OpenScreen(ScreenType.MainMenuLayer);
        }
        #endregion
    }
}
