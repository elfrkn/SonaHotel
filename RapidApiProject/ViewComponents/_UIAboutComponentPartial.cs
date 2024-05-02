using Microsoft.AspNetCore.Mvc;

namespace RapidApiProject.ViewComponents
{
    public class _UIAboutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
