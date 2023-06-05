﻿using MIAUDataBase.Services.DTO;

namespace MIAUDataBase.Services.Abstracts
{
    /// <summary>
    /// Нужен для DI. 
    /// </summary>
    public interface ILogSetService : ISetService<LogDto>
    {
        public Task<List<LogDto>> GetAllForServerAsync(string serverId, int page, int itemsPerPage);
        public Task<int> GetNumberOfLogsById(string serverPatientId, int page, int itemsPerPage);

    }
}
