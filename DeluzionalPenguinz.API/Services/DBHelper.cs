using DeluzionalPenguinz.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DeluzionalPenguinz.API.Services
{
    public class DBHelper
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DBHelper(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task MigrateIfNotAlready()
        {


            await applicationDbContext.Database.EnsureCreatedAsync();
            bool migrationsExist = (await applicationDbContext.Database.GetPendingMigrationsAsync()).Any();
            if (migrationsExist)
            await  applicationDbContext.Database.MigrateAsync();
        }

    }
}
