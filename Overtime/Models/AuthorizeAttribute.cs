using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime.Models
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(string permission)
        : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}
