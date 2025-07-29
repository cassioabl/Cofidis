using Cofidis.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Cofidis.Infra
{
    public class CofidisDbContext : DbContext
    {
        public CofidisDbContext()
        {
        }

        public CofidisDbContext(DbContextOptions<CofidisDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:sql-carpintaria.database.windows.net,1433;Initial Catalog=db-carpintaria;Persist Security Info=False;User ID=sql-carpintaria;Password=Ca$$1oCABL;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
    }
}
