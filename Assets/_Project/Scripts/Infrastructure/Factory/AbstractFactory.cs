using Utility.DependencyInjection;

namespace Infrastructure.Factory
{
    public abstract class AbstractFactory
    {
        #region FIELDS PRIVATE
        protected static DependencyContainer _container;
        protected static ComponentResolver _resolver;
        #endregion

        #region METHODS PUBLIC
        public static void Initialization(DependencyContainer container, ComponentResolver resolver)
        {
            _container = container;
            _resolver = resolver;
        }
        #endregion
    }
}
