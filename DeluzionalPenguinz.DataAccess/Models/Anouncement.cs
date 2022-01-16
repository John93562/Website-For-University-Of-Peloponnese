namespace DeluzionalPenguinz.DataAccess.Models
{
    public class Anouncement : DomainObject
    {
        public Anouncement(int id) : base(id)
        {

        }
        public Anouncement(int id, string? title, Human? professor, Course? course) : base(id)
        {
            this.Title = title;
            this.Professor = professor;
            this.Course = course;
        }

        public string? Title { get; set; }
        public  Human? Professor { get; set; }
        public Course? Course { get; set; }
    }
}
