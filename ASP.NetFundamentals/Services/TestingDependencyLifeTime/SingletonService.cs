using ASP.NetFundamentals.Interfaces.ITestingDependencyLifeTime;

namespace ASP.NetFundamentals.Services.TestingDependencyLifeTime
{
    public class SingletonService : ISingletonService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
