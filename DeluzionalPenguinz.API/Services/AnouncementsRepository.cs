using DeluzionalPenguinz.DataAccess.Models;
using DeluzionalPenguinz.DataAccess.Services;
using DeluzionalPenguinz.Models.Identity;

namespace DeluzionalPenguinz.API.Services
{
    public class AnouncementsRepository
    {
        AnouncementsDataService anouncementsDataService;
        public AnouncementsRepository()
        {

        }

        public AnouncementsRepository(AnouncementsDataService anouncementsDataService)
        {
            this.anouncementsDataService = anouncementsDataService;
        }

        internal async Task<IEnumerable<AnouncementDTO>> GetAllAnouncements()
        {
            var allAnnouncements = await anouncementsDataService.GetAll();

            if (allAnnouncements is null || allAnnouncements.Any() is false)
                return new List<AnouncementDTO>();

            List<AnouncementDTO> anouncementDTOs = new List<AnouncementDTO>();

            foreach (var anouncement in allAnnouncements)
            {
                HumanDTO humanDTO = new HumanDTO(anouncement.Professor.Id, anouncement.Professor.UserName, anouncement.Professor.FirstName, anouncement.Professor.LastName, string.Empty, HumanType.Professor, null);
                CourseDTO courseDTO = new CourseDTO(anouncement.Course.Id, anouncement.Course.Name);
                anouncementDTOs.Add(new AnouncementDTO(anouncement.Id, anouncement.Title, humanDTO, courseDTO));
            }
            return anouncementDTOs;


        }

        internal async Task<BooleanResponse> AddNewAnouncement(AnouncementDTO anouncement)
        {
            throw new NotImplementedException();
        }

        internal async Task<BooleanResponse> UpdateAnAnouncement(AnouncementDTO anouncement)
        {
            throw new NotImplementedException();
        }

        internal async Task<BooleanResponse> DeleteAnAnouncement(int id)
        {
            throw new NotImplementedException();
        }
    }
}
