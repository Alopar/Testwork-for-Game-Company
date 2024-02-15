using System.Collections.Generic;
using Services.ScreenSystem;
using Utility.DependencyInjection;
using Screens.Layers.Purchase;

namespace Services.PurchaseSystem
{
    public class PurchaseSystem : IPurchaseService
    {
        #region FIELDS PRIVATE
        [Inject] private readonly IScreenService _screenService;
        private readonly List<PurchaseCard> _purchases;
        #endregion

        #region CONSTRUCTORS
        public PurchaseSystem(List<PurchaseCard> purchases)
        {
            _purchases = purchases;
        }
        #endregion

        #region METHODS PUBLIC
        public void PurchaseOffer(int productNumber)
        {
            // Some very smart logic purchase definition...
            var purchase = _purchases[UnityEngine.Random.Range(0, _purchases.Count)];
            var purchaseModel = CreatePurchaseModel(purchase, productNumber);
            _screenService.OpenScreen(ScreenType.PurchaseModal, purchaseModel);
        }
        #endregion

        #region METHODS PRIVATE
        private PurchaseModel CreatePurchaseModel(PurchaseCard purchaseCard, int productNumber)
        {
            var card = purchaseCard;
            var productModels = CreateProductModels(card, productNumber);
            var purchaseModel = new PurchaseModel(
                card.Title,
                card.Description,
                card.PromoSprite,
                card.Price,
                card.Discount,
                productModels
            );

            return purchaseModel;
        }

        private List<ProductModel> CreateProductModels(PurchaseCard card, int productNumber)
        {
            var counter = productNumber;
            var products = new List<ProductModel>();
            foreach (var product in card.Products)
            {
                if (counter == 0) break;

                var model = new ProductModel(product.Card.Name, product.Card.Icon, product.Amount);
                products.Add(model);

                counter--;
            }

            return products;
        }
        #endregion
    }
}
