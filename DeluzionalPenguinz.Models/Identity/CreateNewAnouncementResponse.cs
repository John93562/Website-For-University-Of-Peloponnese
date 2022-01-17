namespace DeluzionalPenguinz.Models.Identity
{
    public class CreateNewAnouncementResponse
    {
        public CreateNewAnouncementResponse(string humanUsername,string humanName, string title, string body, string courseName)
        {
            HumanName = humanName;
            HumanUsername = humanUsername;
            Title = title;
            Body = body;
            CourseName = courseName;
        }
        public string HumanName { get; set; }
        public string HumanUsername { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CourseName { get; set; }
    }
}
