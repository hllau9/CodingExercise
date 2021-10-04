using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodingExercise.DAL;
using CodingExercise.Models;

namespace CodingExercise.Controllers
{
    public class UserAPIController : ApiController
    {
        private readonly IUserStore _userStore;
        private readonly IRoleStore _roleStore;
        private readonly IUserRoleStore _userRoleStore;

        public UserAPIController()
        {
        }

        public UserAPIController(IUserStore userStore, IRoleStore roleStore, IUserRoleStore userRoleStore)
        {
            _userStore = userStore;
            _roleStore = roleStore;
            _userRoleStore = userRoleStore;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            List<UserVM> userList = new List<UserVM>();
            
            var users = _userStore.GetUsers();

            foreach (var user in users)
            {
                //var userRoles = _userRoleStore.GetUserRoles(user);
                userList.Add(new UserVM
                {
                    Id = user.Id,
                    RoleId = 0,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    RoleName = "dummy role",
                    Email = user.Email,
                    Phone = user.Phone,
                    Username = user.Username
                });
            }
            
            return Ok(userList);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var user = _userStore.GetUserById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
