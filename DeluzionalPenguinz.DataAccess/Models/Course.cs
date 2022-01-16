namespace DeluzionalPenguinz.DataAccess.Models
{
    public class Course : DomainObject
    {
        public Course(int id) : base(id)
        {

        }
        public Course(string? name,int id):base(id)
        { 
            Name = name;
        }

        public string? Name { get; set; }
    }
}
