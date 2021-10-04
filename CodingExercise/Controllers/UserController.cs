using CodingExercise.DAL;
using CodingExercise.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingExercise.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserStore _userStore;
        private readonly IRoleStore _roleStore;
        private readonly IUserRoleStore _userRoleStore;

        public UserController()
        {
        }
        public UserController(IUserStore userStore, IRoleStore roleStore, IUserRoleStore userRoleStore)
        {
            _userStore = userStore;
            _roleStore = roleStore;
            _userRoleStore = userRoleStore;
        }
        // GET: User
        public ActionResult Index(int? page)
        {
            List<UserVM> userList = new List<UserVM>();

            var users = _userStore.GetUsers();

            foreach (var user in users)
            {
                var userRoleId = _userRoleStore.GetUserRoles(user).FirstOrDefault().RoleId;
                var roleName = _roleStore.GetRolesById(userRoleId).FirstOrDefault().Name;

                userList.Add(new UserVM
                {
                    Id = user.Id,
                    RoleId = 0,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    RoleName = roleName, 
                    Email = user.Email,
                    Phone = user.Phone,
                    Username = user.Username
                }); ;
            }

            var pagedUserList = userList.ToPagedList(page ?? 1, 3);
            return View(userList.ToPagedList(page ?? 1, 3));
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            //UserData userData = new UserData();
            //IEnumerable<UserRole> roleList = userData.GetUserRoles();

            var roleList = _roleStore.GetRoles(); 

            var user = new UserVM();
            user.RoleNameList = roleList;

            return View(user);
        }

        //// POST: User/Create
        //[HttpPost]
        //public ActionResult Create(UserVM user)
        //{
        //    try
        //    {
        //        //repopulate the dropdownlist options
        //        UserData userData = new UserData();
        //        IEnumerable<UserRole> roleList = userData.GetUserRoles();
        //        user.RoleNameList = roleList;

        //        //to check if the user is creating an admin
        //        if (user.RoleId == 1 && string.IsNullOrEmpty(user.Phone))
        //        {
        //            ModelState.AddModelError("Phone", "Phone no is required for an admin user.");
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            return View(user);
        //        }

        //        var retValue = userData.AddUser(user.Username, user.LastName, user.FirstName, user.RoleId, user.Email, user.Phone);

        //        //insert failure
        //        if (retValue < 1)
        //        {
        //            ModelState.AddModelError("", "Error: User creation failed.");
        //            return View(user);
        //        }

        //        ViewBag.Message = @"The user has been created successfully.";
        //        return View(user);
        //        //return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult ExportToPDF()
        //{
        //    UserData userData = new UserData();
        //    var userDT = userData.PopulateUserDataTable();

        //    string fileName = "List_of_Users_" + Guid.NewGuid().ToString() + ".pdf";
        //    string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), "Temp", fileName);

        //    Helpers.Helpers.ExportToPdf(userDT, filePath);

        //    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

        //    return File(fileBytes, "System.Net.Mime.MediaTypeNames.Application.Octet", fileName);

        //    //return Content("Exported to " + filePath);
        //}

        //public ActionResult ExportToExcel()
        //{
        //    UserData userData = new UserData();
        //    var userDT = userData.PopulateUserDataTable();

        //    string fileName = "List_of_Users_" + Guid.NewGuid().ToString() + ".xlsx";
        //    string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), "Temp", fileName);

        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(userDT);

        //    Helpers.Helpers.ExportToExcel(ds, filePath);

        //    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}
    }
}