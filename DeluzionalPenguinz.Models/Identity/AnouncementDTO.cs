namespace DeluzionalPenguinz.Models.Identity
{

    public class AnouncementDTO
    {
        public AnouncementDTO()
        {

        }
        public AnouncementDTO(int id, string? title,string? body,DateTime date, HumanDTO? professor, CourseDTO? course)
        {
            Id = id;
            Title = title;
            this.Body = body;
            Date = date;
            Professor = professor;
            Course = course;
        }

        public int Id { get; set; }
        public string? Body { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public HumanDTO? Professor { get; set; }
        public CourseDTO? Course { get; set; }
    }

}
