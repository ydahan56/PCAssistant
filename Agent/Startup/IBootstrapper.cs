using FluentScheduler;

namespace Agent.Startup
{
    public interface IBootstrapper
    {
        Registry GeInstance();
    }
}