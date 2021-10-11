using CodingExercise.Entities;
using CodingExercise.Models.Account;
using CodingExercise.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Linq;

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

            ClaimsIdentity identity = new ClaimsIdentity(claims, "MyApplicationCookie");

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

            TempData["SuccessMsg"] = "The registration was successful. Please log in.";
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut("MyApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = AuthenticationManager.GetExternalLoginInfo();
            var externalId = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
            
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, loginInfo.DefaultUserName));
            claims.Add(new Claim(ClaimTypes.Name, loginInfo.DefaultUserName));
            claims.Add(new Claim(ClaimTypes.Email, loginInfo.Email));

            // the Issuer of a new ClaimsIdentity default to LOCAL AUTHORITY. To retain the original issuer e.g. Google, the Issuer has to be set explicitly 
            var issuer = loginInfo.ExternalIdentity.Claims.Select(c => c.Issuer).FirstOrDefault();
            claims.Add(new Claim(type: "Issuer", issuer));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "MyApplicationCookie");

            IOwinContext context = Request.RequestContext.HttpContext.Request.GetOwinContext();
            context.Authentication.SignIn(identity);

            return new RedirectResult(returnUrl);
        }



        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}