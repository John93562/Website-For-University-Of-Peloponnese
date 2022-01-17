using DeluzionalPenguinz.Models.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DeluzionalPenguinz.UOP.Pages
{
    public partial class AnouncementModel
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public int? Id { get; set; } = 0;

        [Parameter]
        public bool? Create { get; set; }

        [Parameter]
        public bool? Edit { get; set; }

        AnouncementDTO Anouncement { get; set; }

        bool isLoading { get; set; }


        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; }

        bool CanEdit { get; set; }


        CreateNewAnouncementResponse AnouncementToCreate { get; set; } = new CreateNewAnouncementResponse("", "", "", "", "");
        UpdateAnouncementResponse AnouncementToUpdate { get; set; } = new UpdateAnouncementResponse(0, "", "", "", "", "");

        async Task HandleAnouncementUpdate()
        {

        


            var result = await AnouncementsService.UpdateAnouncement(AnouncementToUpdate);
            NavigationManager.NavigateTo("/AllAnouncements");
        }
        async Task HandleAnouncementCreate()
        {
            

            var result = await AnouncementsService.AddAnouncement(AnouncementToCreate);
            NavigationManager.NavigateTo("/AllAnouncements");
        }

        void EditPost()
        {
            Edit = true;
            AnouncementToUpdate = new UpdateAnouncementResponse(Anouncement.Id, Anouncement.Professor.Username, Anouncement.Professor.FullName, Anouncement.Title, Anouncement.Body, Anouncement.Course.Name);
        }
        string username { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            isLoading = true;
            var result = await AuthenticationState;
            if (result.User is null || result.User.Identity is null || !result.User.Identity.IsAuthenticated)
                NavigationManager.NavigateTo("/");



            //if (Create.HasValue && Create==true)
            //{
            //    //Anouncement = new AnouncementDTO(0,"","",DateTime.Now,new HumanDTO(0,);

            //}
            username = result.User.Identity.Name;


            string HumanTypeValue = result.User.Claims.FirstOrDefault(e => e.Type == "HumanType")?.Value;

            if (HumanTypeValue=="Professor" && Create.HasValue && Create == true)
            {
               
                CourseDTO newCourse = new CourseDTO(0, "");
                string HumanName = result.User.Claims.FirstOrDefault(e => e.Type == "HumanFullName")?.Value;
               

                AnouncementToCreate = new CreateNewAnouncementResponse(username, HumanName, "", "", "");
                isLoading = false;

                return;

            }
            if (Id.HasValue == false && string.IsNullOrEmpty(Title))
                NavigationManager.NavigateTo("/AllAnouncements");
            if (Id.HasValue == false || Id < 0)
            {
                NavigationManager.NavigateTo("/AllAnouncements");
                return;
            }
            try
            {
                Anouncement = await AnouncementsService.GetSingleAnouncement(Id.Value);
            }
            catch (Exception)
            {
                NavigationManager.NavigateTo("/AllAnouncements");
                return;

            }




            if (Anouncement.Professor.Username != username )
            {
                Edit = null;
            }

            if (Anouncement is null)
            {
                NavigationManager.NavigateTo("/AllAnouncements");
                return;
            }
            else if (Edit.HasValue)
            {
                string HumanName = result.User.Claims.FirstOrDefault(e => e.Type == "HumanFullName")?.Value;

                AnouncementToUpdate = new UpdateAnouncementResponse(Anouncement.Id, username, HumanName, Anouncement.Title, Anouncement.Body, Anouncement.Course.Name);
            }

            if (Anouncement.Professor.Username == username)
                CanEdit = true;

            isLoading = false;
        }


        void NavigateToAllAnouncements()
        {
            NavigationManager.NavigateTo("/AllAnouncements");
        }
    }
}
