using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CodingExercise.CustomAttributes;
using CodingExercise.Entities;

namespace CodingExercise.Models
{
    public class UserVM
    {
        [ExcludeFromExport]
        public int Id { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }


        //foreign key
        [ExcludeFromExport]
        [Required]
        public int RoleId { get; set; }

        [DisplayName("Role")]
        public string RoleName { get; set; }

        //role dropdown list
        [ExcludeFromExport]
        [DisplayName("Role")]
        public IEnumerable<Role> RoleNameList { get; set; }
    }
}