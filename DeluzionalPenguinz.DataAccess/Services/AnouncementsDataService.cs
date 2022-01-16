using DeluzionalPenguinz.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.DataAccess.Services
{
    public class AnouncementsDataService
    {
        private readonly DbContextOptions<ApplicationDbContext> dbContextOptions;

        public AnouncementsDataService(DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;


        }

        public async Task<Anouncement> Create(Anouncement Entity)
        {
            try
            {

                using (ApplicationDbContext context = new ApplicationDbContext(dbContextOptions))
                {

                    EntityEntry<Anouncement> createdResult = await context.AddAsync(Entity);
                    await context.SaveChangesAsync();
                    return createdResult.Entity;

                }
            }
            catch (Exception)
            {

            }
            return null;
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext(dbContextOptions))
                {
                    Anouncement entity = await context.Anouncements.FirstOrDefaultAsync(e => e.Id == Id);

                    context.Remove(entity);


                    await context.SaveChangesAsync();


                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;

        }


        public async Task<Anouncement> Update(int Id, Anouncement entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext(dbContextOptions))
            {
                try
                {
                    entity.Id = Id;
                    context.Update(entity);

                    await context.SaveChangesAsync();

                    return entity;
                }
                catch (Exception)
                {

                }
                return null;

            }
        }


        public async Task<IEnumerable<Anouncement>> GetAll()
        {

            using (ApplicationDbContext context = new ApplicationDbContext(dbContextOptions))
            {
                IEnumerable<Anouncement> entities = await context.Anouncements
                    .ToListAsync();

                return entities;
            }
        }


        public async Task<Anouncement> GetById(int Id)
        {

            using (ApplicationDbContext context = new ApplicationDbContext(dbContextOptions))
            {
                Anouncement entity = await context.Anouncements
                    .FirstOrDefaultAsync(e => e.Id == Id);

                return entity;
            }
        }




    }
}
