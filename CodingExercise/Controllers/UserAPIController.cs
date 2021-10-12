using CodingExercise.Models;
using CodingExercise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CodingExercise.APIControllers
{
    public class UserController : ApiController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IUserService _userService;

        public UserController()
        {
        }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
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
            
            return Ok(userList);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                var userRoleId = 0;

                if (user == null)
                    return Ok("User not found.");

                userRoleId = _userService.GetUserRoles(user).FirstOrDefault().RoleId;
                var roleName = _userService.GetRolesById(userRoleId).FirstOrDefault().Name;

                UserVM userVM = new UserVM
                {
                    Id = user.Id,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    RoleName = roleName,
                    Email = user.Email,
                    Phone = user.Phone
                };

                return Ok(user);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Ok("Something went wrong.");
            }
        }
    }
}
