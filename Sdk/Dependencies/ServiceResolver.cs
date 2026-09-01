using SimpleInjector;

namespace Sdk.Dependencies
{
    public class ServiceResolver : IServiceResolver
    {
        private readonly Container _container;

        public ServiceResolver(Container container)
        {
            _container = container;
        }

        public TService ResolveInstance<TService>() where TService : class
        {
            return this._container.GetInstance<TService>();
        }

        public IEnumerable<TService> ResolveInstances<TService>() where TService : class
        {
            return this._container.GetAllInstances<TService>();
        }
    }
}
