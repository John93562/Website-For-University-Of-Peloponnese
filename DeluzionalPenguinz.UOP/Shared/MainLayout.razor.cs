using DeluzionalPenguinz.Models.Identity;
using DeluzionalPenguinz.UOP.StaticResources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace DeluzionalPenguinz.UOP.Shared
{
    public partial class MainLayout
    {
        bool isProfessor { get; set; }
        bool MenuIsOpen { get; set; } = false;
        string HumanFullName { get; set; } = "";
        string enableclass { get; set; } = "";
        public MainLayout()
        {
            ApplicationStaticResources.DoActionAfterAuthorization += AuthorizeBasedIfItIsTheProfessor;
        }
        async Task ToggleMenu()
        {
            MenuIsOpen = !MenuIsOpen;
            await CallJSMethod(MenuIsOpen);


        }

        async Task NavigateToAllAnouncements()
        {
            NavigationManager.NavigateTo("/AllAnouncements");
            MenuIsOpen = false;
            await CallJSMethod(false);
        }
        async Task NavigateToProfessorsAnouncements()
        {
            NavigationManager.NavigateTo("/MyAnouncements");
            MenuIsOpen = false;

            await CallJSMethod(false);
        }

        async Task Logout()

        {
            MenuIsOpen = false;

            await CallJSMethod(false);
            await LocalStorage.RemoveItemAsync("Token");
            await AuthStateProvider.GetAuthenticationStateAsync();
            NavigationManager.NavigateTo("/");
        }

        public void AuthorizeBasedIfItIsTheProfessor(HumanType humanType, string humanFullName)
        {
            if (humanType == HumanType.Professor)
                isProfessor = true;

            HumanFullName = humanFullName;
        }
        protected async Task CallJSMethod(bool show)
        {
            await JSRuntime.InvokeVoidAsync("JSMethod", show);
        }

    }
}
