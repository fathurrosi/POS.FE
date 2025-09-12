using Microsoft.AspNetCore.Mvc;
using POS.Presentation.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Presentation.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(string screen = null)
       : base(typeof(AuthorizeFilter))
        {
            Screen = screen;
            Arguments = new object[] { screen };
        }

        public string Screen { get; set; }
    }

}
