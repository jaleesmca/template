using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class AuthorizeActionFilter: IAuthorizationFilter
    {
        private readonly string _permission;

        public AuthorizeActionFilter(string permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = CheckUserPermission(context.HttpContext.User, _permission);

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool CheckUserPermission(ClaimsPrincipal user, string permission)
        {
            // Logic for checking the user permission goes here. 

            // Let's assume this user has only read permission.
            return permission == "Read";
        }
    }
}
