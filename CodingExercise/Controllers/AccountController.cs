using CodingExercise.Entities;
using CodingExercise.Models.Account;
using CodingExercise.Services;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CodingExercise.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPasswordService _passwordService;
        private readonly IUserService _userService;

        public AccountController(IPasswordService passwordService, IUserService userService)
        {
            _passwordService = passwordService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var appUser = _userService.GetUserByEmail(loginVM.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid login. Please try again.");
                return View(loginVM);
            }

            string passwordHash = _userService.GetUserByEmail(loginVM.Email).PasswordHash;

            var verified = _passwordService.VerifyHashedPassword(passwordHash, loginVM.Password);

            if (!verified)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid login. Please try again.");
                return View(loginVM);
            }

            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Email));
            claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));
            claims.Add(new Claim(ClaimTypes.Name, appUser.Email));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie");

            IOwinContext context = Request.RequestContext.HttpContext.Request.GetOwinContext();
            context.Authentication.SignIn(identity);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
                return View(registerVM);

            var userExists = _userService.GetUserByEmail(registerVM.Email);
            if (userExists != null)
            {
                ModelState.AddModelError("Email", "User with that email address already exists.");
                return View(registerVM);
            }

            var addUserSuccessful = _userService.AddUser(new AppUser
                                                        {
                                                            Email = registerVM.Email,
                                                            LastName = registerVM.LastName,
                                                            FirstName = registerVM.FirstName,
                                                            Phone = registerVM.Phone,
                                                            PasswordHash = _passwordService.HashPassword(registerVM.Password)
                                                        });

            if (!addUserSuccessful)
            {
                ModelState.AddModelError("AddUserError", "User registration was not successful.");
                return View(registerVM);
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}