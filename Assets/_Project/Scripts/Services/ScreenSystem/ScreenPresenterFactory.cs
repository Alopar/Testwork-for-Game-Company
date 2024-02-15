using Screens.Layers.Main;
using Screens.Layers.Purchase;
using Utility.DependencyInjection;

namespace Services.ScreenSystem
{
    public class ScreenPresenterFactory
    {
        #region METHODS PUBLIC
        public IScreenPresenter Create(ScreenType screenType, AbstractView view)
        {
            //TODO: non compliance open-close principle, refactoring needed
            IScreenPresenter presenter = null;
            switch (screenType)
            {
                case ScreenType.MainMenuLayer:
                    presenter = new MainMenuPresenter(view as MainMenuView);
                    break;
                case ScreenType.PurchaseModal:
                    presenter = new PurchasePresenter(view as PurchaseView);
                    break;
            }
            DependencyContainer.Inject(presenter);

            return presenter;
        }
        #endregion
    }
}
