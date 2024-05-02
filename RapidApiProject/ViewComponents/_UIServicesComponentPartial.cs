using Microsoft.AspNetCore.Mvc;

namespace RapidApiProject.ViewComponents
{
    public class _UIServicesComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
