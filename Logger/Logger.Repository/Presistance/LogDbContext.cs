using Logger.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logger.Repository.Presistance
{
    public class LogDbContext : DbContext
    {
        public DbSet<EmailLog> EmailLogs { get; set; }

        public LogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }
    }
}
