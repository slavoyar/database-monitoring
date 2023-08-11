using Hangfire.Dashboard;

namespace Agregation.Infrastructure.Services.Implementations
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public HangfireAuthorizationFilter()
        {
        }

        public bool Authorize(DashboardContext context)
        {
            return true; //I'am returning true for simplicity
        }
    }
}