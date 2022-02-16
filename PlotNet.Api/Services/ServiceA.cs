namespace PlotNet.Api;

public class ServiceA : IServiceA
{
    private readonly IServiceB _serviceB;

    public ServiceA(IServiceB serviceB)
    {
        _serviceB = serviceB;
    }

    public void Invoke()
    {
        _serviceB.Invoke();
    }

}