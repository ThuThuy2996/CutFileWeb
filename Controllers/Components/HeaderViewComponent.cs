using CutFileWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CutFileWeb.Controllers.Components
{
    public class HeaderViewComponent :  ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {           
            return View("_Header");
        }
    }
}

