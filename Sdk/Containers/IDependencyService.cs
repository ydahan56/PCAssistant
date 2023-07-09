﻿namespace Sdk.Containers
{
    public interface IDependencyService
    {
        T ResolveInstance<T>() where T : class;
        IEnumerable<T> ResolveInstances<T>() where T : class;
        void RegisterInstance<T>(T instance) where T : class;
        void RegisterInstances<T>(IEnumerable<T> instances) where T : class;

        void Verify();
    }
}
