using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Models;

namespace OneBanTMS.IntegrationTests
{
    public class DbContextFactory
    {
        public static OneManDbContext GetOneManDbContext()
        {
            var connectionString = "Server=tcp:onebantms.database.windows.net,1433;Initial Catalog=OneBanDb;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            return new OneManDbContext(optionBuilder.Options);
        }
    }
}