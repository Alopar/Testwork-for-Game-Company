using System;
using UnityEngine;

namespace Screens.Layers.Purchase
{
    public class ProductModel
    {
        #region FIELDS PRIVATE
        private readonly string _name;
        private readonly Sprite _icon;
        private readonly int _number;
        #endregion

        #region PROPERTIES
        public string Name => _name;
        public Sprite Icon => _icon;
        public int Number => _number;
        #endregion

        #region EVENTS
        public event Action OnDataUpdated;
        #endregion

        #region CONSTRUCTORS
        public ProductModel(string name, Sprite icon, int number)
        {
            _name = name;
            _icon = icon;
            _number = number;
        }
        #endregion
    }
}
