using System.Collections.Generic;
using UnityEngine;
using Services.ScreenSystem;

namespace Screens.Layers.Purchase
{
    public class PurchasePresenter : IScreenPresenter
    {
        #region FIELDS PRIVATE
        private readonly PurchaseView _view;
        private PurchaseModel _model;
        #endregion

        #region CONSTRUCTORS
        public PurchasePresenter(PurchaseView view)
        {
            _view = view;
            _view.OnClickCloseButton += ClickCloseButton;
            _view.OnClickBuyButton += ClickBuyButton;
        }
        #endregion

        #region METHODS PUBLIC
        public void OpenScreen(object payload = null)
        {
            var model = payload as PurchaseModel;
            if (model == null)
            {
                Debug.LogError("PurchasePresenter: not correct set payload");
                return;
            }

            if (_model != null)
            {
                _model.OnDataUpdated -= ModelDataUpdated;
            }

            _model = model;
            _model.OnDataUpdated += ModelDataUpdated;

            ModelDataUpdated();
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
        private void ModelDataUpdated()
        {
            _view.UpdateView(
                _model.Title,
                _model.Description,
                _model.PromoSprite,
                _model.Price,
                _model.Discount,
                _model.Products as List<ProductModel>
                );
        }

        private void ClickCloseButton()
        {
            _view.HideScreen();
        }

        private void ClickBuyButton()
        {
            _view.HideScreen();
            Debug.Log("Begin in-app transaction...");
        }
        #endregion
    }
}
