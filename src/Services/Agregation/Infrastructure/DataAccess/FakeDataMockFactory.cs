using Agregation.Domain.Models;

namespace Agregation.Infrastructure.DataAccess
{
    public class FakeDataMockFactory
    {
        public ServerPatient GenerateFakeData() {
            var servPatient = GenerateFakeEmptyServerPatient();
            GenerateFakeLog(servPatient);
            GenerateFakeLog(servPatient);
            GenerateFakeLog(servPatient);
            GenerateFakeLog(servPatient);
            return servPatient;
        }

        public ServerPatient GenerateFakeEmptyServerPatient()
        {
            var servPatient = new ServerPatient {
                Id = Guid.NewGuid(),
                ConnectionStatus = true,
                IconId = "qw",
                IdAddress = "we",
                LastSuccessLog = DateTime.Now.ToString(),
                Logs = new List<Log>(),
                Name = "Server 1",
                PingStatus = true,
                Status = "er"
            };
            return servPatient;
        }

        public List<Log> GenerateFakeLogs(ServerPatient serverPatient) 
        {
            var logList = new List<Log>();
            for (var i = 0; i < 3; i++) 
            {
                logList.Add(GenerateFakeLog(serverPatient));
            }
            return logList;
        }

        private static Log GenerateFakeLog(ServerPatient serverPatient)
        {
            var log = new Log() {
                CreationDate = DateTime.Now.ToString(),
                CriticalStatus = "qw",
                ErrorState = "error",
                Id = Guid.NewGuid(),
                Message = "qwqwqww",
                RecievedAt = DateTime.Now.ToString(),
                //Server = servPatient,
                ServerPatientId = serverPatient.Id,
                ServiceName = "aasa",
                ServiceType = "asas"
            };
            //serverPatient.Logs.Add(log);//Если добавить до выдаст ошибку, что не может засеить (seeding) данные  
            return log;
        }


    }
}
