namespace Graft.Api;

public class ServiceC : IServiceC
{
    private readonly IServiceD _serviceD;

    public ServiceC(IServiceD serviceD)
    {
        _serviceD = serviceD;
    }

    public void Invoke()
    {
        _serviceD.Invoke();
    }
}