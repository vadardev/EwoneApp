using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Domain.Requests.Module;

public class EditModuleRequestHandler : IRequestHandler<EditModuleRequest, Result<EditModuleResponse>>
{
    private readonly IUnitToWork _unitToWork;

    public EditModuleRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result<EditModuleResponse>> Handle(EditModuleRequest request, CancellationToken cancellationToken)
    {
        var module = await _unitToWork.Modules.GetAsync(x => x.Id == request.Request.ModuleId, cancellationToken);

        if (module == null || module.UserId != request.UserId)
        {
            return new Result<EditModuleResponse>
            {
                Error = "Module not found"
            };
        }

        module.Name = request.Request.Body.Name;
        module.Description = request.Request.Body.Description;

        await _unitToWork.CommitAsync();

        return new Result<EditModuleResponse>
        {
            Value = new EditModuleResponse
            {
                ModuleId = module.Id,
            },
        };
    }
}

public class EditModuleRequest : IRequest<Result<EditModuleResponse>>
{
    public int UserId { get; set; }
    public EditModuleExternalRequest Request { get; set; } = null!;
}

public class EditModuleBody
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class EditModuleExternalRequest
{
    [FromRoute(Name = "moduleId")] public int ModuleId { get; set; }
    [FromBody] public EditModuleBody Body { get; set; } = null!;
}

public class EditModuleResponse
{
    public int ModuleId { get; set; }
}