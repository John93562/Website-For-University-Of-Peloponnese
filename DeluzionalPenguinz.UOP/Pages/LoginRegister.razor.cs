using DeluzionalPenguinz.Models.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace DeluzionalPenguinz.UOP.Pages
{


    public partial class LoginRegister
    {

        string rotatedClass = "";

        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; }
        public UserLogin UserToLogin { get; set; }
        public UserRegister UserToRegister { get; set; }

        string isWhoIsItFrameActive { get; set; } = "active-registration";
        string isUserInfoFrameActive { get; set; } = "";
        string isPasswordFrameActive { get; set; } = "";
        bool isWhoIsItFrameVisible { get; set; } = true;
        bool isUserInfoFrameVisible { get; set; }
        bool isPasswordFrameVisible { get; set; }

        void GoToNextFrame()
        {

       
                if (isWhoIsItFrameVisible)
            {
                isWhoIsItFrameVisible = false;
                isPasswordFrameVisible = false;
                isUserInfoFrameVisible = true;

                isWhoIsItFrameActive = "";
                isPasswordFrameActive = "";
                isUserInfoFrameActive = "active-registration";

            }
            else if (isUserInfoFrameVisible)
            {
                if (string.IsNullOrEmpty(UserToRegister.Email) || string.IsNullOrEmpty(UserToRegister.FirstName) || string.IsNullOrEmpty(UserToRegister.LastName))
                {
                    if (editContext != null)
                        editContext.Validate();
                    return;
                }
                isWhoIsItFrameVisible = false;
                isUserInfoFrameVisible = false;
                isPasswordFrameVisible = true;

                isWhoIsItFrameActive = "";
                isUserInfoFrameActive = "";
                isPasswordFrameActive = "active-registration";
            }
         
        }
        void GoToPreviousFrame()
        {
           
            if (isPasswordFrameVisible)
            {
                isWhoIsItFrameVisible = false;
                isPasswordFrameVisible = false;
                isUserInfoFrameVisible = true;
                isWhoIsItFrameActive = "";
                isPasswordFrameActive = "";
                isUserInfoFrameActive = "active-registration";
            }
            else if (isUserInfoFrameVisible)
            {
                isUserInfoFrameVisible = false;
                isPasswordFrameVisible = false;
                isWhoIsItFrameVisible = true;
                isWhoIsItFrameActive = "active-registration";
                isPasswordFrameActive = "";
                isUserInfoFrameActive = "";
            }
        }
        protected override async Task OnInitializedAsync()
        {
            var result = await AuthenticationState;

            if (result.User is not null && result.User.Identity is not null && result.User.Identity.IsAuthenticated)
                NavigationManager.NavigateTo("/AllAnouncements");
            editContext = new EditContext(UserToRegister);
        }
        public LoginRegister()
        {
            UserToLogin = new UserLogin();
            UserToRegister = new UserRegister();
        }

        void DoNothing()
        {

        }
        public async Task HandleLogin()
        {
            var response = await AuthenticationService.Login(UserToLogin);

            if (response is not null && !string.IsNullOrEmpty(response.Token))
                NavigationManager.NavigateTo("/AllAnouncements");
        }

        public async Task HandleRegister()
        {
            if (!isPasswordFrameVisible)
                return;
            if (editContext != null && editContext.Validate())
            {
                var response = await AuthenticationService.Register(UserToRegister);
            }
            else
            {
                isWhoIsItFrameActive = "active-registration";
                isPasswordFrameActive = "";
                isUserInfoFrameActive = "";
                isPasswordFrameVisible = false;
                isUserInfoFrameVisible = false;
                isWhoIsItFrameVisible = true;
             
            }
        }
        void Rotate()
        {
            bool rotatedIsAlreadyOn = rotatedClass == "rotated";

            if (rotatedIsAlreadyOn)
                rotatedClass = "";
            else
                rotatedClass = "rotated";

        }
        private EditContext? editContext;


    }
}
