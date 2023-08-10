﻿using Agregation.Domain.Intefaces;
using Agregation.Domain.Models;
using Agregation.Infrastructure.DataAccess;
using Agregation.Infrastructure.Services.Abstracts;
using AutoMapper.Configuration.Annotations;
using TestPatient.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

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
            for (int i = 0; i < 60; i++)
            {
                var patientEndpoint = _configuration.GetValue<string>("PatientEndpoint") ?? throw new NullReferenceException("PatientEndpoint from appsettings is null");

                try
                {
                    var servers = _serverPatientSetService.GetPagedAsync(1, 100).Result;

                    foreach (var server in servers)
                    {
                        var stateChanged = false;
                        var serverDataEndpoint = new Uri("http://" + server.IdAddress + "/" + patientEndpoint);

                        var _httpClient = new HttpClient();

                        var httpResponse = _httpClient.GetAsync(serverDataEndpoint).Result;

                        if (!httpResponse.IsSuccessStatusCode)
                            throw new Exception($"{serverDataEndpoint} is not reachable)");

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

                        if (logsList.Count > 0)
                        {
                            stateChanged = true;
                            _AppDatabaseContext.Logs.AddRange(logsList);
                            _AppDatabaseContext.SaveChanges();
                        }

                        // Send new data to SignalR Group
                        if (stateChanged)
                        {
                            var newServerState = _serverPatientSetService.GetAsync(new Guid(server.Id));
                            var serverInJson = JsonSerializer.Serialize(newServerState);
                            _hubContext.Clients.Group(server.Id).SendAsync("Receive", serverInJson).Wait();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                var random = new Random();
                var randomValue = random.Next(500, 1000);
                Task.Delay(randomValue).Wait();
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