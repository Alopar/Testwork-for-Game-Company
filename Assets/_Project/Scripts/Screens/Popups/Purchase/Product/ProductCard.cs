using UnityEngine;

namespace Screens.Layers.Purchase
{
    [CreateAssetMenu(fileName = "ProductCard", menuName = "Purchase/ProductCard", order = 0)]
    public class ProductCard : ScriptableObject
    {
        #region FIELDS INSPECTOR
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        #endregion

        #region PROPERTIES
        public string Name => _name;
        public Sprite Icon => _icon;
        #endregion
    }
}
