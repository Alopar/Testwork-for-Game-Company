namespace Utility.DependencyInjection
{
    public abstract class AbstractDependency
    {
        #region FIELDS PRIVATE
        protected static DependencyContainer _container;
        #endregion

        #region METHODS PUBLIC
        public static void Initialization(DependencyContainer container)
        {
            _container = container;
        }
        #endregion
    }
}
