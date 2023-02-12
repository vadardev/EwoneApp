using Ewone.Domain.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Api.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet]
    public Task<GetUsersResponse> GetUsers([FromRoute] GetUsersRequest request)
    {
        return _mediator.Send(request);
    }
}