using Ewone.Domain.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public Task<LoginResponse> Login([FromBody] LoginRequest request)
    {
        return _mediator.Send(request);
    }
}