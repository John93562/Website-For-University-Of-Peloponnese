namespace DeluzionalPenguinz.DataAccess.Models
{
    public class Anouncement : DomainObject
    {
        public Anouncement(int id) : base(id)
        {

        }
        public Anouncement(int id, string? title,string? body,DateTime date, string? professorId, Course? course) : base(id)
        {
            this.Date = date;
            this.Title = title;
            this.Body = body;
            this.ProfessorSomething = professorId;
            this.Course = course;
        }

        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? ProfessorSomething { get; set; }
        public DateTime Date { get; set; }
        public Course? Course { get; set; }
    }
}
