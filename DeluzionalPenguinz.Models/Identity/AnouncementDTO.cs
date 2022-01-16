namespace DeluzionalPenguinz.Models.Identity
{

    public class AnouncementDTO
    {
        public AnouncementDTO()
        {

        }
        public AnouncementDTO(int id, string? title, HumanDTO? professor, CourseDTO? course)
        {
            Id = id;
            Title = title;
            Professor = professor;
            Course = course;
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public HumanDTO? Professor { get; set; }
        public CourseDTO? Course { get; set; }
    }

}
