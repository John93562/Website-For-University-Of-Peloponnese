using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DeluzionalPenguinz.UOP.Pages
{
    public partial class AllAnouncements
    {
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var result = await AuthenticationState;


            if (result.User is null ||  result.User.Identity is null || !result.User.Identity.IsAuthenticated)
                NavigationManager.NavigateTo("/");

        }
    }
}
