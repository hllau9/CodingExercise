using CodingExercise.Entities;
using CodingExercise.Models;
using CodingExercise.Services;
using CodingExercise.Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;

namespace CodingExercise.Controllers
{
    public class UserController : Controller
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        public ActionResult Index(int? page)
        {
            if (page < 1)
                page = 1;

            List<UserVM> userList = new List<UserVM>();

            try
            {
                var users = _userService.GetUsers();

                foreach (var user in users)
                {
                    var userRoleId = _userService.GetUserRoles(user).FirstOrDefault().RoleId;
                    var roleName = _userService.GetRolesById(userRoleId).FirstOrDefault().Name;

                    userList.Add(new UserVM
                    {
                        Id = user.Id,
                        RoleId = 0,
                        LastName = user.LastName,
                        FirstName = user.FirstName,
                        RoleName = roleName,
                        Email = user.Email,
                        Phone = user.Phone,
                    }); ;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return View(userList.ToPagedList(page ?? 1, 3));
        }

        /*
        public ActionResult Create()
        {
            var roleList = _userService.GetRoles(); 

            var user = new UserVM();
            user.RoleNameList = roleList;

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserVM userVM)
        {
            try
            {
                //repopulate the dropdownlist options
                var roleList = _userService.GetRoles();
                userVM.RoleNameList = roleList;

                if (!ModelState.IsValid)
                {
                    return View(userVM);
                }

                var user = new AppUser
                {
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Email = userVM.Email,
                    Phone = userVM.Phone
                };

                var userExists = _userService.GetUserByEmail(userVM.Email);
                if (userExists != null)
                {
                    ModelState.AddModelError("", "Email already exists.");
                    throw new Exception();
                }

                var successful = _userService.AddUser(user);
                if (!successful)
                {
                    throw new Exception();
                }

                ViewBag.Message = @"The user has been created successfully.";

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Error: User creation failed.");
                return View(userVM);
            }
        }
        */

        public ActionResult UserListApiVersion()
        {
            return View();
        }

        public ActionResult ExportToPDF()
        {
            List<UserVM> userList = new List<UserVM>();

            var users = _userService.GetUsers();

            foreach (var user in users)
            {
                var userRoleId = _userService.GetUserRoles(user).FirstOrDefault().RoleId;
                var roleName = _userService.GetRolesById(userRoleId).FirstOrDefault().Name;

                userList.Add(new UserVM
                {
                    Id = user.Id,
                    RoleId = 0,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    RoleName = roleName,
                    Email = user.Email,
                    Phone = user.Phone,
                }); ;
            }

            var dt = Export.PopulateDataTable(userList);

            string fileName = "List_of_Users_" + Guid.NewGuid().ToString() + ".pdf";
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), "Temp", fileName);

            Export.ExportToPdf(dt, filePath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "System.Net.Mime.MediaTypeNames.Application.Octet", fileName);
        }

        public ActionResult ExportToExcel()
        {
            List<UserVM> userList = new List<UserVM>();

            var users = _userService.GetUsers();

            foreach (var user in users)
            {
                var userRoleId = _userService.GetUserRoles(user).FirstOrDefault().RoleId;
                var roleName = _userService.GetRolesById(userRoleId).FirstOrDefault().Name;

                userList.Add(new UserVM
                {
                    Id = user.Id,
                    RoleId = 0,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    RoleName = roleName,
                    Email = user.Email,
                    Phone = user.Phone,
                }); ;
            }

            var dt = Export.PopulateDataTable(userList);

            string fileName = "List_of_Users_" + Guid.NewGuid().ToString() + ".xlsx";
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), "Temp", fileName);

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(dt);

            Export.ExportToExcel(ds, filePath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "System.Net.Mime.MediaTypeNames.Application.Octet", fileName);
        }
    }
}