using Sdk.Dependencies;
using SimpleInjector;

namespace Sdk.Containers
{

    /*
     * Basically, this class just abstracts away the SimpleInjector container.
     * We don't want to be passing around the Container itself, so we wrap it in this class.
    */

    public class DependencyLocator : IServiceLocator
    {
        private readonly Container _container;

        public DependencyLocator(Container container)
        {
            this._container = container;
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
