using CodingExercise.Helpers;
using System;

namespace CodingExercise.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            return Password.HashPassword(password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return Password.VerifyHashedPassword(hashedPassword, providedPassword);
        }
    }
}
