﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CodingExercise.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
