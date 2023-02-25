using Common;
using Ewone.Domain.Requests.Module;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Api.Controllers;

[Authorize]
[ApiController]
[Route("modules")]
public class ModuleController : BaseController
{
    private readonly IMediator _mediator;

    public ModuleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("default")]
    [AllowAnonymous]
    public Task<Result<GetDefaultModuleResponse>> GetDefaultModule([FromRoute] GetDefaultModuleRequest request)
    {
        return _mediator.Send(request);
    }

    [HttpPost]
    public Task<Result<AddModuleResponse>> AddModule([FromRoute] AddModuleExternalRequest request)
    {
        return _mediator.Send(new AddModuleRequest
        {
            UserId = GetUserId(),
            Request = request,
        });
    }

    [HttpPatch("{moduleId}")]
    public Task<Result<EditModuleResponse>> EditModule([FromRoute] EditModuleExternalRequest request)
    {
        return _mediator.Send(new EditModuleRequest
        {
            UserId = GetUserId(),
            Request = request,
        });
    }
    //
    [HttpDelete("{moduleId}")]
    public Task<Result<DeleteModuleResponse>> DeleteModule([FromRoute] DeleteModuleExternalRequest request)
    {
        return _mediator.Send(new DeleteModuleRequest
        {
            UserId = GetUserId(),
            Request = request,
        });
    }

    [HttpGet]
    public Task<Result<GetModulesResponse>> GetModules()
    {
        return _mediator.Send(new GetModulesRequest
        {
            UserId = GetUserId(),
        });
    }
}