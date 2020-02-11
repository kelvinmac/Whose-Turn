using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Whose_Turn.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Gets the Guid of the current user
        /// </summary>
        public Guid UserId
        {
            get
            {
                // Get object-identifier's claim value
                var str = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.Parse(str);
            }
        }
    }
}
