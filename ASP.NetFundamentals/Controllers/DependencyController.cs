using ASP.NetFundamentals.Interfaces.ITestingDependencyLifeTime;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetFundamentals.Controllers
{
    public class DependencyController : Controller
    {
        private readonly ISingletonService _singletonService1;
        private readonly ISingletonService _singletonService2;

        private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;

        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;

        public DependencyController(
            ISingletonService singletonService1,
            ISingletonService singletonService2,
            IScopedService scopedService1,
            IScopedService scopedService2,
            ITransientService transientService1,
            ITransientService transientService2)
        {
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;

            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;

            _transientService1 = transientService1;
            _transientService2 = transientService2;
        }

        public IActionResult Index()
        {
            ViewBag.SingletonId1 = _singletonService1.InstanceId;
            ViewBag.SingletonId2 = _singletonService2.InstanceId;

            ViewBag.ScopedId1 = _scopedService1.InstanceId;
            ViewBag.ScopedId2 = _scopedService2.InstanceId;

            ViewBag.TransientId1 = _transientService1.InstanceId;
            ViewBag.TransientId2 = _transientService2.InstanceId;

            return View();
        }
    }
}

