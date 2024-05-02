using Microsoft.AspNetCore.Mvc;

namespace RapidApiProject.ViewComponents
{
    public class _UISearchComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
