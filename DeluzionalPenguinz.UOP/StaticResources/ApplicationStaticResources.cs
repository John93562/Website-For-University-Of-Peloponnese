using DeluzionalPenguinz.Models.Identity;

namespace DeluzionalPenguinz.UOP.StaticResources
{
    public class ApplicationStaticResources
    {
        public static event Action<HumanType, string> DoActionAfterAuthorization;
        public static void InvokeActionAfterAuthorization(HumanType humanType, string HumanName)
        {
            try
            {
                DoActionAfterAuthorization?.Invoke(humanType, HumanName);
            }
            catch (Exception ex)
            {

            }

        }



    }
}
