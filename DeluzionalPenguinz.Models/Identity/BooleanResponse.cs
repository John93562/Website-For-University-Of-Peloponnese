namespace DeluzionalPenguinz.Models.Identity
{
    public class BooleanResponse
    {
        public BooleanResponse(bool success)
        {
            Success = success;
        }

        public bool Success { get; set; }
    }
}
