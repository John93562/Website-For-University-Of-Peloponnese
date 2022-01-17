namespace DeluzionalPenguinz.Models.Identity
{
    public class UpdateAnouncementResponse : CreateNewAnouncementResponse
    {
        public int AnouncementId { get; set; }

        public UpdateAnouncementResponse(int anouncementId,string humanUsername, string humanName, string title, string body, string courseName) : base(humanUsername, humanName, title, body, courseName)
        {
            AnouncementId = anouncementId;
        }
    }
}
