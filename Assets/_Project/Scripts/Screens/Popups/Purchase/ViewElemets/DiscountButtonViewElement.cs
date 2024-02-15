using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Screens.Layers.Purchase
{
    public class DiscountButtonViewElement : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;

        [Space(10)]
        [SerializeField] private GameObject _lable;
        [SerializeField] private TextMeshProUGUI _lableText;
        #endregion

        #region PROPERTIES
        public Button.ButtonClickedEvent onClick => _button.onClick;
        #endregion

        #region METHODS PUBLIC
        public void UpdateViewElem(float price, float discount)
        {
            SetButtonText(price, discount);
            SetLabelText(discount);
        }
        #endregion

        #region METHODS PRIVATE
        private void SetButtonText(float price, float discount)
        {
            if(discount == 0)
            {
                _buttonText.text = $"${price:F2}";
            }
            else
            {
                var discountPrice = price * discount;
                var finalPrice = price - discountPrice;
                _buttonText.text = $"${finalPrice:F2}\n<size=70%><color=#97986E><s>${price:F2}</s></color>";
            }
        }

        private void SetLabelText(float discount)
        {
            if(discount == 0)
            {
                _lable.SetActive(false);
            }
            else
            {
                _lable.SetActive(true);
                _lableText.text = $"{0 - discount:P0}";
            }
        }
        #endregion
    }
}
