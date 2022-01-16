namespace DeluzionalPenguinz.Models.Identity
{
    public class CourseDTO
    {
        public CourseDTO()
        {

        }
        public CourseDTO(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
    }

}
