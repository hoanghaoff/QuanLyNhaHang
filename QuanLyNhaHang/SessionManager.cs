using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyNhaHang.Models;

namespace QuanLyNhaHang
{
    public static class SessionManager
    {
        public static Users CurrentUser { get; private set; }
        public static IReadOnlyCollection<string> CurrentRoles => _roles.AsReadOnly();
        private static List<string> _roles = new List<string>();

        public static bool IsSignedIn => CurrentUser != null;

        public static void SignIn(Users user)
        {
            CurrentUser = user;
            _roles.Clear();

            if (user?.UserRoles != null)
            {
                foreach (var ur in user.UserRoles)
                {
                    if (ur?.Roles?.Name != null && !_roles.Contains(ur.Roles.Name))
                        _roles.Add(ur.Roles.Name);
                }
            }
        }

        public static void SignOut()
        {
            CurrentUser = null;
            _roles.Clear();
        }

        public static bool IsInRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName)) return false;
            return _roles.Contains(roleName);
        }
    }
}
