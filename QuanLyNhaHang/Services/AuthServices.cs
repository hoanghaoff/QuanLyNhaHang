using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using QuanLyNhaHang.Models;
using System.Data.Entity;

namespace QuanLyNhaHang.Services
{
    public static class AuthService
    {
        // Compute SHA256 hex string for a plain text password
        public static string ComputeHash(string plain)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(plain ?? string.Empty);
                var hash = sha.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach (var b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // Authenticate user by username + password; returns Users entity with roles loaded or null
        public static Users Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return null;

            var hash = ComputeHash(password);

            using (var ctx = new RestaurentContextDB())
            {
                // load user and roles
                var user = ctx.Users
                    .Include(u => u.UserRoles.Select(ur => ur.Roles))
                    .FirstOrDefault(u => u.Username == username && u.IsActive && !u.IsDeleted);

                if (user == null) return null;

                // Compare stored PasswordHash (assumed hex lower/upper) with computed
                if (string.Equals(user.PasswordHash?.Trim(), hash, StringComparison.OrdinalIgnoreCase))
                {
                    return user;
                }

                return null;
            }
        }
    }
}
