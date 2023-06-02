using Microsoft.AspNetCore.Mvc;

namespace Blogger_Web.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
