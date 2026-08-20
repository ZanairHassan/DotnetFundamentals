using ASP.NetFundamentals.Interfaces.ITestingDependencyLifeTime;

namespace ASP.NetFundamentals.Services.TestingDependencyLifeTime
{
    public class ScopedService : IScopedService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
