using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace CodingPlatform.Web.Controllers;

public class CustomBaseController : ControllerBase
{
    protected Guid GetCurrentUserId()
    {
        return Guid.Parse(
            HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
        );
    }
}