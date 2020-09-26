using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IOWebFramework.Components
{
    public class MainMenuComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string currentItem = "")
        {
            return await Task.FromResult<IViewComponentResult>(View("", currentItem));
        }
    }
}
