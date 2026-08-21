namespace ASP.NetFundamentals.Interfaces.ITestingDependencyLifeTime
{
    public interface ISingletonService
    {
        Guid InstanceId { get; }
    }
}
