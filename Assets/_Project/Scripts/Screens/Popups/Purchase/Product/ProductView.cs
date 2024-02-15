using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Screens.Layers.Purchase
{
    public class ProductView : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _numberText;
        #endregion

        #region METHODS PUBLIC
        public void UpdateView(Sprite icon, int number)
        {
            _icon.sprite = icon;
            _numberText.text = number.ToString();
        }
        #endregion
    }
}
