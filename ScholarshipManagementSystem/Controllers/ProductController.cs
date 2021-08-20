using Microsoft.AspNetCore.Mvc;

namespace ScholarshipManagementSystem.Controllers
{
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
}