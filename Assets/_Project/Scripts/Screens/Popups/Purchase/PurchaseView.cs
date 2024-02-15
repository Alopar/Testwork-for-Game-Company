using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Services.ScreenSystem;

namespace Screens.Layers.Purchase
{
    public class PurchaseView : AbstractView
    {
        #region FIELDS INSPECTOR
        [Space(10)]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Image _promoImage;
        [SerializeField] private Button _closeButton;
        [SerializeField] private DiscountButtonViewElement _discountButton;
        [SerializeField] private StorefrontViewElement _storefront;
        #endregion

        #region EVENTS
        public event Action OnClickBuyButton;
        public event Action OnClickCloseButton;
        #endregion

        #region METHODS PUBLIC
        public void UpdateView(string title, string description, Sprite promoSprite, float price, float discount, List<ProductModel> products)
        {
            _titleText.text = title;
            _descriptionText.text = description;
            _promoImage.sprite = promoSprite;

            _storefront.UpdateViewElem(products);
            _discountButton.UpdateViewElem(price, discount);
        }
        #endregion

        #region HANDLERS
        private void CloseButtonClick()
        {
            OnClickCloseButton?.Invoke();
        }

        private void BuyButtonClick()
        {
            OnClickBuyButton?.Invoke();
        }
        #endregion

        #region UNITY CALLBACKS
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseButtonClick);
            _discountButton.onClick.AddListener(BuyButtonClick);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseButtonClick);
            _discountButton.onClick.RemoveListener(BuyButtonClick);
        }
        #endregion
    }
}
