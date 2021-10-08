using CodingExercise.CustomAttributes;
using CodingExercise.DAL;
using System.Security.Claims;
using System.Web.Mvc;

namespace CodingExercise.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ClaimsAuthorize(ClaimTypes.Name, "super@super.com")]
        public ActionResult Restricted()
        {
            ViewBag.Message = "This is a restricted page accessible only by super@super.com.";

            return View();
        }
    }
}