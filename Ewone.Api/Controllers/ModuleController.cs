using Common;
using Ewone.Domain.Requests.Module;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Api.Controllers;

[ApiController]
[Route("module")]
public class ModuleController : BaseController
{
    private readonly IMediator _mediator;

    public ModuleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("default")]
    public Task<Result<GetDefaultModuleResponse>> GetDefaultModule([FromRoute] GetDefaultModuleRequest request)
    {
        return _mediator.Send(request);
    }

    [HttpPost]
    [Authorize]
    public Task<Result<AddModuleResponse>> AddModule([FromRoute] AddModuleExternalRequest request)
    {
        return _mediator.Send(new AddModuleRequest
        {
            UserId = GetUserId(),
            Request = request,
        });
    }

    [HttpPatch]
    public Task<Result<EditModuleResponse>> EditModule([FromRoute] EditModuleExternalRequest request)
    {
        return _mediator.Send(new EditModuleRequest
        {
            UserId = GetUserId(),
            Request = request,
        });
    }
    //
    // [HttpDelete]
    // public Task DeleteModule()
    // {
    // }
}