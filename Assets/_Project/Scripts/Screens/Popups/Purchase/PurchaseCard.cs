using UnityEngine;
using System.Collections.Generic;

namespace Screens.Layers.Purchase
{
    [CreateAssetMenu(fileName = "PurchaseCard", menuName = "Purchase/PurchaseCard", order = 1)]
    public class PurchaseCard : ScriptableObject
    {
        #region FIELDS INSPECTOR
        [SerializeField] private string _title;
        [SerializeField, TextArea] private string _description;

        [Space(10)]
        [SerializeField] private Sprite _promoSprite;

        [Space(10)]
        [SerializeField, Min(0)] private float _price;
        [SerializeField, Range(0, 1)] private float _discount;

        [Space(10)]
        [SerializeField] private List<ProductDTO> _products;
        #endregion

        #region PROPERTIES
        public string Title => _title;
        public string Description => _description;
        public Sprite PromoSprite => _promoSprite;

        public float Price => _price;
        public float Discount => _discount;
        
        public IEnumerable<ProductDTO> Products => _products;
        #endregion
    }
}
