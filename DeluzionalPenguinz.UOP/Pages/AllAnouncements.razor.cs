using DeluzionalPenguinz.Models.Identity;
using DeluzionalPenguinz.UOP.StaticResources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DeluzionalPenguinz.UOP.Pages
{
    public partial class AllAnouncements
    {
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; }

        bool isLoading { get; set; }


        private string selectedCourseName;
        public string SelectedCourseName
        {
            get
            {
                return selectedCourseName;
            }
            set
            {
                selectedCourseName = value;
                FilterBasedOnCourse(value);
            }
        }

        private string selectedProfessorName;
        public string SelectedTeacherName
        {
            get
            {
                return selectedProfessorName;
            }
            set
            {
                selectedProfessorName = value;
                FilterBasedOnTeacher(value);
            }
        }

        public List<string> AllProfessorNames { get; set; } = new List<string>();
        public List<string> AllCoursesNames { get; set; } = new List<string>();

        public List<AnouncementDTO> AllAnouncementsModels { get; set; } = new List<AnouncementDTO>();
        public List<AnouncementDTO> AllAnouncementsModelsFiltered { get; set; } = new List<AnouncementDTO>();
        void NavigateToAnouncement(int? id, string? title)
        {
            if (id.HasValue == false)
                return;
            NavigationManager.NavigateTo($"/Anouncement/{id}/{title}");

        }

        Func<IEnumerable<AnouncementDTO>, IEnumerable<AnouncementDTO>> TeacherFilter { get; set; }
        Func<IEnumerable<AnouncementDTO>, IEnumerable<AnouncementDTO>> CourseFilter { get; set; }

        void FilterBasedOnTeacher(string name)
        {
            if (name == "Όλοι οι Καθηγητές")
            {
                TeacherFilter = null;
            }
            else
            {
                TeacherFilter = e => e.Where(s => s.Professor.FullName == name);
            }
            FilterAnouncements();
        }
        void FilterBasedOnCourse(string name)
        {
            if (name == "Όλα τα Μαθήματα")
            {
                CourseFilter = null;
            }
            else
            {
                CourseFilter = e => e.Where(s => s.Course.Name == name);
            }
            FilterAnouncements();
        }

        void FilterAnouncements()
        {

            IEnumerable<AnouncementDTO> allAnouncementsTemp = AllAnouncementsModels;

            if (TeacherFilter is not null)
                allAnouncementsTemp = TeacherFilter(allAnouncementsTemp);


            if (CourseFilter is not null)
                allAnouncementsTemp = CourseFilter(allAnouncementsTemp);

            AllAnouncementsModelsFiltered.Clear();


            foreach (var item in allAnouncementsTemp)
            {
                AllAnouncementsModelsFiltered.Add(item);
            }


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

            string HumanTypeValue = result.User.Claims.FirstOrDefault(e => e.Type == "HumanType")?.Value;

            string HumanName = result.User.Claims.FirstOrDefault(e => e.Type == "HumanFullName")?.Value;

            if (string.IsNullOrEmpty(HumanTypeValue))
                return;

            if (HumanTypeValue == "Professor")
                humanType = HumanType.Professor;

            ApplicationStaticResources.InvokeActionAfterAuthorization(humanType, HumanName);


            IEnumerable<AnouncementDTO> allAnouncements = await AnouncementsService.GetAllAnouncements();

            foreach (AnouncementDTO anouncement in allAnouncements.OrderByDescending(e=>e.Date))
            {
                if (!AllProfessorNames.Contains(anouncement.Professor.FullName))
                    AllProfessorNames.Add(anouncement.Professor.FullName);

                if (!AllCoursesNames.Contains(anouncement.Course.Name))
                    AllCoursesNames.Add(anouncement.Course.Name);

                AllAnouncementsModels.Add(anouncement);
                AllAnouncementsModelsFiltered.Add(anouncement);
            }
            isLoading = false;
        }
    }
}
