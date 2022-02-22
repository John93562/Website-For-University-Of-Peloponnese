using DeluzionalPenguinz.Models.Identity;
using DeluzionalPenguinz.UOP.StaticResources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DeluzionalPenguinz.UOP.Pages
{
    public partial class MyAnouncements
    {
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; }


        public List<AnouncementDTO> ProfessorsAnouncements { get; set; } = new List<AnouncementDTO>();

        bool isLoading { get; set; }

        string FullProfessorName { get; set; }

        string Username { get; set; }
        void AddNewAnouncement()
        {
            NavigationManager.NavigateTo($"/Anouncement/true");
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            isLoading = true;
            var result = await AuthenticationState;
            HumanType humanType = HumanType.Student;


            if (result.User is null || result.User.Identity is null || !result.User.Identity.IsAuthenticated)
                NavigationManager.NavigateTo("/");

            string HumanTypeValue = result.User.Claims.FirstOrDefault(e => e.Type == "HumanType")?.Value;

            string HumanName = result.User.Claims.FirstOrDefault(e => e.Type == "HumanFullName")?.Value;

            if (string.IsNullOrEmpty(HumanTypeValue))
                return;

            if (HumanTypeValue == "Professor")
                humanType = HumanType.Professor;

            ApplicationStaticResources.InvokeActionAfterAuthorization(humanType, HumanName);
        }
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            var result = await AuthenticationState;
            HumanType humanType = HumanType.Student;


            if (result.User is null || result.User.Identity is null || !result.User.Identity.IsAuthenticated)
                NavigationManager.NavigateTo("/");



            string HumanName = result.User.Claims.FirstOrDefault(e => e.Type == "HumanFullName")?.Value;

            string HumanTypeValue = result.User.Claims.FirstOrDefault(e => e.Type == "HumanType")?.Value;

            if (string.IsNullOrEmpty(HumanTypeValue))
                return;

            if (HumanTypeValue == "Professor")
                humanType = HumanType.Professor;

            ApplicationStaticResources.InvokeActionAfterAuthorization(humanType, HumanName);


            if (humanType != HumanType.Professor)
                NavigationManager.NavigateTo("/AllAnouncements");

            FullProfessorName = HumanName;
            Username = result.User.Identity.Name;

            IEnumerable<AnouncementDTO> allAnouncements = await AnouncementsService.GetAllAnouncements();

            foreach (AnouncementDTO anouncement in allAnouncements.Where(e => e.Professor.Username == result.User.Identity.Name).OrderByDescending(e => e.Date))
                ProfessorsAnouncements.Add(anouncement);

            isLoading = false;

        }
        void NavigateToAllAnouncements()
        {
            NavigationManager.NavigateTo("/AllAnouncements");
        }


        async Task DeleteAnouncement(int id)
        {
            isLoading = true;
            var res = await AnouncementsService.DeleteAnouncement(id);
            ProfessorsAnouncements.Clear();

            IEnumerable<AnouncementDTO> allAnouncements = await AnouncementsService.GetAllAnouncements();

            foreach (AnouncementDTO anouncement in allAnouncements.Where(e => e.Professor.Username == Username).OrderByDescending(e => e.Date))
                ProfessorsAnouncements.Add(anouncement);
            isLoading = false;
        }

        void GoToAnouncement(int id,string title)
        {
            NavigationManager.NavigateTo($"/Anouncement/{id}/{title}");
        }
        void EditAnouncement(int id, string title)
        {
            NavigationManager.NavigateTo($"/Anouncement/{id}/{title}/true");
        }

    }
}
