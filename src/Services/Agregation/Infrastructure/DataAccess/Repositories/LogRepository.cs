﻿using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Agregation.Infrastructure.DataAccess.Repositories
{
    public class LogRepository : AbstractRepository<Log>, ILogRepository
    {
        public LogRepository(DbContext context) : base(context)
        {
        }
        private IQueryable<Log> GetQueryPageLogsOfServers(string serverId, int page, int itemsPerPage) { 
            return GetAll()
                    .Where(log => log.ServerId == serverId)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage);
        }

        public List<Log> GetAllForServer(string serverId, int page, int itemsPerPage)
        {
            var query = GetQueryPageLogsOfServers(serverId, page, itemsPerPage);
            return query.ToList();
        }
        public async Task<List<Log>> GetAllForServerAsync(string serverId, int page, int itemsPerPage)
        {
            return await Task.Run(() => GetAllForServer(serverId, page, itemsPerPage));  
        }

        public int GetNumberOfLogsById(string serverPatientId, int page, int itemsPerPage)
        {
            var query = GetAllForServer(serverPatientId, page, itemsPerPage);
            return query.Count;
        }

        public async Task<int> GetNumberOfLogsByIdAsync(string serverPatientId, int page, int itemsPerPage)
        {
            return await Task.Run(() => GetNumberOfLogsById(serverPatientId, page, itemsPerPage));
        }
    }
}