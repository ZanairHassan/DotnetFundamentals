using ASP.NetFundamentals.Interfaces.ITestingDependencyLifeTime;

namespace ASP.NetFundamentals.Services.TestingDependencyLifeTime
{
    public class TransientService : ITransientService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
