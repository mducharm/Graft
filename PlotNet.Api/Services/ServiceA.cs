namespace PlotNet.Api;

public class ServiceA : IServiceA
{
    private readonly IServiceB _serviceB;
    private readonly IServiceC _serviceC;

    public ServiceA(IServiceB serviceB, IServiceC serviceC)
    {
        _serviceB = serviceB;
        _serviceC = serviceC;
    }

    public void Invoke()
    {
        _serviceB.Invoke();
        _serviceC.Invoke();
    }

}