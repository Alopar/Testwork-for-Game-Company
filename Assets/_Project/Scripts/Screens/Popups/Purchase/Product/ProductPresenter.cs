namespace Screens.Layers.Purchase
{
    public class ProductPresenter
    {
        #region FIELDS PRIVATE
        private readonly ProductView _view;
        private readonly ProductModel _model;
        #endregion

        #region CONSTRUCTORS
        public ProductPresenter(ProductView view, ProductModel model)
        {
            _view = view;
            _model = model;

            _model.OnDataUpdated += ModelDataUpdated;
            ModelDataUpdated();
        }
        #endregion

        #region HANDLERS
        private void ModelDataUpdated()
        {
            _view.UpdateView(_model.Icon, _model.Number);
        }
        #endregion
    }
}
