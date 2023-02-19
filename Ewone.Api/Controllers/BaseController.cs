namespace Ewone.Api.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

public class BaseController : ControllerBase
{
    protected int GetUserId()
    {
        return Int32.Parse(User.FindFirstValue(ClaimTypes.Name));
    }
}