namespace ASP.NetFundamentals.Interfaces.ITestingDependencyLifeTime
{
    public interface IScopedService
    {
        Guid InstanceId { get; }
    }
}
