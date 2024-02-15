using UnityEngine;
using Services.ScreenSystem;
using Services.PurchaseSystem;
using Utility.DependencyInjection;

namespace Screens.Layers.Main
{
    public class MainMenuPresenter : IScreenPresenter
    {
        #region FIELDS PRIVATE
        [Inject] private readonly IPurchaseService _purchaseService;
        private readonly MainMenuView _view;

        private int _productNumber = 3;
        #endregion

        #region CONSTRUCTORS
        public MainMenuPresenter(MainMenuView view)
        {
            _view = view;
            _view.OnPurchaseButtonClick += PurchaseButtonClick;
            _view.OnProductInputFieldChanged += ProductInputFieldChanged;
        }
        #endregion

        #region METHODS PUBLIC
        public void OpenScreen(object payload = null)
        {
            _view.ShowScreen();
        }

        public void CloseScreen()
        {
            _view.HideScreen();
        }

        public void DestroyView()
        {
            GameObject.Destroy(_view.gameObject);
        }
        #endregion

        #region HANDLERS
        private void PurchaseButtonClick()
        {
            _purchaseService.PurchaseOffer(_productNumber);
        }

        private void ProductInputFieldChanged(int value)
        {
            _productNumber = value;
        }
        #endregion
    }
}
