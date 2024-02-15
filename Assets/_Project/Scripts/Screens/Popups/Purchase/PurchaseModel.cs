using System;
using System.Collections.Generic;
using UnityEngine;

namespace Screens.Layers.Purchase
{
    public class PurchaseModel
    {
        #region FIELDS PRIVATE
        private readonly string _title;
        private readonly string _description;
        private readonly Sprite _promoSprite;

        private readonly float _price;
        private readonly float _discount;

        private readonly List<ProductModel> _products;
        #endregion

        #region PROPERTIES
        public string Title => _title;
        public string Description => _description;
        public Sprite PromoSprite => _promoSprite;

        public float Price => _price;
        public float Discount => _discount;

        public IEnumerable<ProductModel> Products => _products;
        #endregion

        #region EVENTS
        public event Action OnDataUpdated;
        #endregion

        #region CONSTRUCTORS
        public PurchaseModel(string title, string description, Sprite promoSprite, float price, float discount, List<ProductModel> products)
        {
            _title = title;
            _description = description;
            _promoSprite = promoSprite;
            _price = price;
            _discount = discount;
            _products = products;
        }
        #endregion
    }
}
