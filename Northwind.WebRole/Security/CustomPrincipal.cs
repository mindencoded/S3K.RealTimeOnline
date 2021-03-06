﻿using System.Linq;
using System.Security.Principal;
using System.Threading;

namespace Northwind.WebRole.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly IIdentity _identity;
        private readonly string[] _roles;

        public CustomPrincipal(IIdentity identity, string[] roles)
        {
            _identity = identity;
            _roles = roles;
        }

        public static CustomPrincipal Current
        {
            get { return Thread.CurrentPrincipal as CustomPrincipal; }
        }

        public string[] Roles
        {
            get { return _roles; }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string roles)
        {
            return roles.Split(',').Any(role => _roles.Contains(role));
        }
    }
}