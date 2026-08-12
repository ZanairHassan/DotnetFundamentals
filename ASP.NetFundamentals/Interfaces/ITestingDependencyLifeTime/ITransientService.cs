namespace ASP.NetFundamentals.Interfaces.ITestingDependencyLifeTime
{
    public interface ITransientService
    {
        Guid InstanceId { get; }
    }
}
