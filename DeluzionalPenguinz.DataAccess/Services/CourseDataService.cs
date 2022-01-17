using DeluzionalPenguinz.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DeluzionalPenguinz.DataAccess.Services
{
    public class CourseDataService
    {
        private readonly DbContextOptions<ApplicationDbContext> dbContextOptions;

        public CourseDataService(DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;


        }

        public async Task<Course> Create(Course Entity)
        {
            try
            {

                using (ApplicationDbContext context = new ApplicationDbContext(dbContextOptions))
                {

                    EntityEntry<Course> createdResult = await context.AddAsync(Entity);
                    await context.SaveChangesAsync();
                    return createdResult.Entity;

                }
            }
            catch (Exception εχ)
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
                    Course entity = await context.Courses.FirstOrDefaultAsync(e => e.Id == Id);

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


        public async Task<Course> Update(int Id, Course entity)
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


        public async Task<IEnumerable<Course>> GetAll()
        {

            using (ApplicationDbContext context = new ApplicationDbContext(dbContextOptions))
            {
                IEnumerable<Course> entities = await context.Courses
                    .ToListAsync();

                return entities;
            }
        }


        public async Task<Course> GetById(int Id)
        {

            using (ApplicationDbContext context = new ApplicationDbContext(dbContextOptions))
            {
                Course entity = await context.Courses
                    .FirstOrDefaultAsync(e => e.Id == Id);

                return entity;
            }
        }




    }

}
