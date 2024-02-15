using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Services.ScreenSystem;

namespace Screens.Layers.Main
{
    public class MainMenuView : AbstractView
    {
        #region FIELDS INSPECTOR
        [Space(10)]
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private TMP_InputField _productInputField;
        #endregion

        #region EVENTS
        public event Action OnPurchaseButtonClick;
        public event Action<int> OnProductInputFieldChanged;
        #endregion

        #region HANDLERS
        private void PurchaseButtonClick()
        {
            OnPurchaseButtonClick?.Invoke();
        }

        private void ProductInputFieldChanged(string value)
        {
            if(int.TryParse(value, out int number))
            {
                OnProductInputFieldChanged?.Invoke(number);
            }
        }
        #endregion

        #region UNITY CALLBACKS
        private void OnEnable()
        {
            _purchaseButton.onClick.AddListener(PurchaseButtonClick);
            _productInputField.onValueChanged.AddListener(ProductInputFieldChanged);
        }

        private void OnDisable()
        {
            _purchaseButton.onClick.RemoveListener(PurchaseButtonClick);
            _productInputField.onValueChanged.RemoveListener(ProductInputFieldChanged);
        }
        #endregion
    }
}
