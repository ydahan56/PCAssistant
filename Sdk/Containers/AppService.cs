using SimpleInjector;

namespace Sdk.Containers
{
    public class AppService : IDependencyService
    {
        private readonly Container _container;

        public AppService(Container container)
        {
            _container = container;
        }

        public void RegisterInstance<T>(T instance) where T : class
        {
            this._container.RegisterInstance<T>(instance);
        }

        public void RegisterInstances<T>(IEnumerable<T> instances) where T : class
        {
            this._container.Collection.Register<T>(instances);
        }

        public T ResolveInstance<T>() where T : class
        {
            return this._container.GetInstance<T>();
        }

        public IEnumerable<T> ResolveInstances<T>() where T : class
        {
            return this._container.GetAllInstances<T>();
        }

        public void Verify()
        {
            this._container.Verify();
        }
    }
}
