using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PasswordHelper
{
    public static string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<string>();
        var hashedPassword = passwordHasher.HashPassword(null, password);
        return hashedPassword;
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var passwordHasher = new PasswordHasher<string>();
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
        return passwordVerificationResult == PasswordVerificationResult.Success;
    }
}
