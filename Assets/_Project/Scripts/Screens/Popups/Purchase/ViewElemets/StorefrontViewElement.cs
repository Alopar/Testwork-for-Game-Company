using System.Collections.Generic;
using UnityEngine;

namespace Screens.Layers.Purchase
{
    public class StorefrontViewElement : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private Transform _content;
        [SerializeField] private ProductView _productPrefab;
        #endregion

        #region FIELDS PRIVATE
        private List<ProductPresenter> _presenters = new();
        #endregion

        #region METHODS PUBLIC
        public void UpdateViewElem(List<ProductModel> models)
        {
            ClearContent();

            foreach (var model in models)
            {
                var view = Instantiate(_productPrefab, _content);
                var presenter = new ProductPresenter(view, model);
                _presenters.Add(presenter);
            }
        }
        #endregion

        #region METHODS PRIVATE
        private void ClearContent()
        {
            _presenters.Clear();
            for (var i = 0; i < _content.childCount; i++)
            {
                Destroy(_content.GetChild(i).gameObject);
            }
        }
        #endregion
    }
}
