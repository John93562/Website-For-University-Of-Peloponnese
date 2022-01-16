//using DataAccess;
//using DeluzionalPenguinz.API.Services.IServices;
//using Microsoft.EntityFrameworkCore;
//using Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DeluzionalPenguinz.API.Services
//{
//    public class DBSeeder : IDBSeeder
//    {
//        private readonly ApplicationDbContext appDb;

//        public DBSeeder(ApplicationDbContext appDb)
//        {
//            this.appDb = appDb;
//        }
//        public void SeedDatabase()
//        {
//            try
//            {
//                if (appDb.Database.GetPendingMigrations().Any())
//                {
//                    appDb.Database.Migrate();
//                }

//            }
//            catch (Exception)
//            {

//                throw;
//            }


//            if (appDb.Bananas.Any() == false)
//            {
//                var BananasToSeed = new List<Banana>()
//                {
//                    new Banana(){ Title = "PoutsoMaxos h Banana"},
//                    new Banana(){ Title = "O Paparas h Banana"},
//                    new Banana(){ Title = "Kwlomagos h Banana"},
//                    new Banana(){ Title = "Ksifokwlos h Banana"},
//                    new Banana(){ Title = "Pirotexnimatourgos h Banana"},
//                };

//                appDb.Bananas.AddRangeAsync(BananasToSeed)
//                     .GetAwaiter()
//                     .GetResult();

//                appDb.SaveChanges();

//            }
//        }
//    }
//}
