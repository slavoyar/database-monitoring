﻿using MIAUDataBase.DataBase.Entities;
using MIAUDataBase.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace MIAUDataBase.Infrastructure.Repositories.Implementation
{
    public class LogRepository : AbstractRepository<Log>, ILogRepository
    {
        public LogRepository(DbContext context) : base(context)
        {
        }
        public List<Log> GetAllForServer(string serverId, int itemsPerPage, int page)
        {
            var query = GetAll()
                    .Where(log => log.ServerId == serverId)
                    .Skip((page - 1)*itemsPerPage)
                    .Take(itemsPerPage);
            return query.ToList();
        }
        public async Task<List<Log>> GetAllForServerAsync(string serverId, int itemsPerPage, int page)
        {
            var query = GetAll()
                    .Where(log => log.ServerId == serverId)
                    .Skip((page - 1)*itemsPerPage)
                    .Take(itemsPerPage);
            return await query.ToListAsync();            
        }        
    }
}
