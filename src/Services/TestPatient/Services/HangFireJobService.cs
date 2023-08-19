using TestPatient.Interfaces;
using TestPatient.Data;

namespace TestPatient.Services
{
    public class HangFireJobService : IHangFireJobService
    {
        private readonly HangfireContext _hangfireDatabaseContext;
        private readonly IConfiguration _configuration;

        public HangFireJobService(HangfireContext hangfireDatabaseContext, IConfiguration configuration)
        {
            _hangfireDatabaseContext = hangfireDatabaseContext;
            _configuration = configuration;
        }

        public void FireAndForgetJob()
        {
            var hostId = _configuration.GetValue<string>("HostId") ?? throw new NullReferenceException("HostId is null");
            var testLogs = TestLogs.GetTestLogs(hostId);

            foreach (var log in testLogs)
            {
                _hangfireDatabaseContext.PatientLogs.Add(log);
                _hangfireDatabaseContext.SaveChanges();
            }
        }

        public void ReccuringJob()
        {
            var hostId = _configuration.GetValue<string>("HostId") ?? throw new NullReferenceException("HostId is null");

            for (int i = 0; i < 10; i++)
            {
                var testLogs = TestLogs.GetTestLogs(hostId);
                foreach (var log in testLogs)
                {
                    _hangfireDatabaseContext.PatientLogs.Add(log);
                    _hangfireDatabaseContext.SaveChanges();
                }

                var random = new Random();
                var randomValue = random.Next(3000, 6000);
                Task.Delay(randomValue).Wait();
            }
        }

        public void DelayedJob()
        {
            Console.WriteLine("Hello from a Delayed job!");
        }

        public void ContinuationJob()
        {
            Console.WriteLine("Hello from a Continuation job!");
        }
    }
}