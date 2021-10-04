using CodingExercise.DAL;
using System.Web.Mvc;

namespace CodingExercise.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserStore _userStore;
        private readonly IRoleStore _roleStore;
        private readonly IUserRoleStore _userRoleStore;

        public HomeController()
        {
        }

        public HomeController(IUserStore userStore, IRoleStore roleStore, IUserRoleStore userRoleStore)
        {
            _userStore = userStore;
            _roleStore = roleStore;
            _userRoleStore = userRoleStore;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}