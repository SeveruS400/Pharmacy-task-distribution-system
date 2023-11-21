using Entities.Models;
using Microsoft.AspNetCore.Mvc;


namespace EzcaneBilgiSistemi.Components
{
    public class LoginViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Users user)
        {
            return await Task.FromResult(View(user));
        }
    }
}
