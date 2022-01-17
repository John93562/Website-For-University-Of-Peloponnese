using DeluzionalPenguinz.DataAccess.Models;
using DeluzionalPenguinz.DataAccess.Services;
using DeluzionalPenguinz.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace DeluzionalPenguinz.API.Services
{
    public class AnouncementsRepository
    {
        AnouncementsDataService anouncementsDataService;
        CourseDataService courseDataService;
        private readonly UserManager<Human> userManager;

     
        public AnouncementsRepository(AnouncementsDataService anouncementsDataService, UserManager<Human> userManager,CourseDataService courseDataService)
        {
            this.courseDataService = courseDataService;
            this.anouncementsDataService = anouncementsDataService;
            this.userManager = userManager;
        }

        internal async Task<IEnumerable<AnouncementDTO>> GetAllAnouncements()
        {
            var allAnnouncements = await anouncementsDataService.GetAll();


            if (allAnnouncements is null || allAnnouncements.Any() is false)
                return new List<AnouncementDTO>();

            List<AnouncementDTO> anouncementDTOs = new List<AnouncementDTO>();

            List<Human> allProfessors = new List<Human>();

            foreach (var anouncement in allAnnouncements)
            {
                Human professor = null;
                if (allProfessors.Select(e => e.UserName).Contains(anouncement.ProfessorSomething))
                {
                    professor = allProfessors.FirstOrDefault(e => e.UserName == anouncement.ProfessorSomething);
                }
                else
                {
                    professor = await userManager.FindByNameAsync(anouncement.ProfessorSomething);
                    allProfessors.Add(professor);
                }

                HumanDTO humanDTO = new HumanDTO(professor.Id, professor.UserName, professor.FirstName, professor.LastName, string.Empty, HumanType.Professor, null);
                CourseDTO courseDTO = new CourseDTO(anouncement.Course.Id, anouncement.Course.Name);
                anouncementDTOs.Add(new AnouncementDTO(anouncement.Id, anouncement.Title, anouncement.Body, anouncement.Date, humanDTO, courseDTO));
            }
            return anouncementDTOs;
        }

        internal async Task<AnouncementDTO> GetSingleAnouncement(int id)
        {
            var anouncement = await anouncementsDataService.GetById(id);
            var professor = await userManager.FindByNameAsync(anouncement.ProfessorSomething);


            return new AnouncementDTO(anouncement.Id,anouncement.Title,anouncement.Body,anouncement.Date,
                new HumanDTO(professor.Id, professor.UserName, professor.FirstName, professor.LastName, professor.AM, professor.HumanType,
                new List<AnouncementDTO>()),new CourseDTO(anouncement.Course.Id,anouncement.Course.Name));

        }

        internal async Task<BooleanResponse> AddNewAnouncement(CreateNewAnouncementResponse anouncement)
        {
            var human = await userManager.FindByNameAsync(anouncement.HumanUsername);
            if (human is null)
                return new BooleanResponse(false);
            Course course = null;


            try
            {
                course = (await courseDataService.GetAll()).FirstOrDefault(e => e.Name.ToLower() == anouncement.CourseName.ToLower());
            }
            catch (Exception ex)
            {
                course = new Course(anouncement.CourseName, 0);
            }

            if (course is null)
                course = new Course(anouncement.CourseName, 0);

            if (course.Id == 0)
                course = await courseDataService.Create(course);

            Anouncement newAnouncement = new Anouncement(0, anouncement.Title,anouncement.Body,DateTime.Now, human.UserName, null);

            
            var res = await anouncementsDataService.Create(newAnouncement);
            res.Course = course;

            var upd = await anouncementsDataService.Update(res.Id, res);


            return new BooleanResponse(res==null);
        }

        internal async Task<BooleanResponse> UpdateAnAnouncement(UpdateAnouncementResponse anouncement)
        {
            Anouncement anouncementdb = await anouncementsDataService.GetById(anouncement.AnouncementId);


            if (anouncementdb is null)
                return new BooleanResponse(false);
            anouncementdb.Title = anouncement.Title;
            anouncementdb.Body = anouncement.Body;
            anouncementdb.Date = DateTime.Now;


            if (anouncement.CourseName != anouncementdb.Course.Name)
            {
                Course newCourse = new Course(anouncement.CourseName, 0);

                var courseUpdated = await courseDataService.Create(newCourse);

                anouncementdb.Course = newCourse;

                var an = await anouncementsDataService.Update(anouncementdb.Id, anouncementdb);

                return an is null ? new BooleanResponse(false) : new BooleanResponse(true);
            }
            else
            {

                var an = await anouncementsDataService.Update(anouncementdb.Id, anouncementdb);

                return an is null ? new BooleanResponse(false) : new BooleanResponse(true);
            }
        }

        internal async Task<BooleanResponse> DeleteAnAnouncement(int id)
        {
            Anouncement anouncementdb = await anouncementsDataService.GetById(id);


            if (anouncementdb is null)
                return new BooleanResponse(false);
            var response = await anouncementsDataService.Delete(anouncementdb.Id);

            return new BooleanResponse(response);


        }
    }
}
