using Microsoft.AspNetCore.Mvc;

namespace RapidApiProject.ViewComponents
{
    public class _UIScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
