using Agregation.Domain.Intefaces;
using Agregation.Domain.Models;
using Agregation.Infrastructure.DataAccess;
using Agregation.Infrastructure.Services.Abstracts;
using Agregation.Infrastructure.Services.DTO;
using Microsoft.AspNetCore.SignalR;
using TestPatient.Models;

namespace Agregation.Infrastructure.Services.Implementations
{
    public class HangFireJobService : IHangFireJobService
    {
        private readonly ApplicationContext _AppDatabaseContext;
        private readonly IConfiguration _configuration;
        private readonly IServerPatientSetService _serverPatientSetService;
        private readonly IHubContext<ServerStateHub> _hubContext;

        public HangFireJobService(
            ApplicationContext AppDatabaseContext,
            IConfiguration configuration,
            IServerPatientSetService serverPatientSetService,
            IHubContext<ServerStateHub> hubContext)
        {
            _AppDatabaseContext = AppDatabaseContext;
            _configuration = configuration;
            _serverPatientSetService = serverPatientSetService;
            _hubContext = hubContext;
        }

        public void FireAndForgetJob()
        {
        }

        public void ReccuringJob()
        {
            var patientEndpoint = _configuration.GetValue<string>("PatientEndpoint") ?? throw new NullReferenceException("PatientEndpoint from appsettings is null");
            var servers = _serverPatientSetService.GetPagedAsync(1, 100).Result;
            ShortServerPatientDto? newServerState = null;

            foreach (var server in servers)
            {
                try
                {
                    newServerState = _serverPatientSetService.GetShortAsync(server.Id).Result;

                    var serverDataEndpoint = new Uri("http://" + server.IdAddress + "/" + patientEndpoint);
                    var _httpClient = new HttpClient();
                    var httpResponse = _httpClient.GetAsync(serverDataEndpoint).Result;

                    if (!httpResponse.IsSuccessStatusCode || newServerState == null)
                    {
                        if (newServerState != null && newServerState.Status != ServerState.Warn)
                        {
                            newServerState.Status = ServerState.Warn;
                            _serverPatientSetService.UpdateStatusAsync(newServerState).Wait();
                            _hubContext.Clients.Group(server.Id.ToString()).SendAsync("Receive", newServerState).Wait();
                        }
                        continue;
                    }

                    var serverLogs = httpResponse.Content.ReadFromJsonAsync<List<SendingLogsModel>>().Result;

                    var logsList = new List<Log>();
                    foreach (var log in serverLogs)
                    {
                        var newLog = new Log()
                        {
                            Id = new Guid(log.Id),
                            CriticalStatus = log.CriticalStatus,
                            ErrorState = log.ErrorState,
                            ServiceType = log.ServiceType,
                            ServiceName = log.ServiceName,
                            CreationDate = log.CreatedAt,
                            Message = log.Message,
                            ServerPatientId = new Guid(log.ServerId)
                        };
                        logsList.Add(newLog);
                    }

                    var areNewLogs = logsList.Count > 0;

                    if (areNewLogs)
                    {
                        _AppDatabaseContext.Logs.AddRange(logsList);
                        _AppDatabaseContext.SaveChanges();
                    }

                    newServerState = _serverPatientSetService.GetShortAsync(server.Id).Result;

                    // Send new data to SignalR Group
                    if (newServerState.Status != ServerState.Working)
                    {
                        newServerState.Status = ServerState.Working;
                        _serverPatientSetService.UpdateStatusAsync(newServerState).Wait();
                        _hubContext.Clients.Group(server.Id.ToString()).SendAsync("Receive", newServerState).Wait();
                    }

                }
                catch (Exception ex)
                {
                    if (newServerState != null && newServerState.Status != ServerState.Down)
                    {
                        newServerState.Status = ServerState.Down;
                        _serverPatientSetService.UpdateStatusAsync(newServerState).Wait();
                        _hubContext.Clients.Group(newServerState.Id.ToString()).SendAsync("Receive", newServerState).Wait();
                    }
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DelayedJob()
        {
        }

        public void ContinuationJob()
        {
        }
    }
}