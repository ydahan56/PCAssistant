namespace Sdk.Dependencies
{
    public interface IServiceResolver
    {
        TService ResolveInstance<TService>() where TService : class;
        IEnumerable<TService> ResolveInstances<TService>() where TService : class;
    }
}