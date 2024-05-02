using Microsoft.AspNetCore.Mvc;

namespace RapidApiProject.ViewComponents
{
    public class _UIHeadComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
