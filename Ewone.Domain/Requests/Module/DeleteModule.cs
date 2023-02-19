using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Ewone.Domain.Requests.Module;

public class DeleteModuleRequestHandler : IRequestHandler<DeleteModuleRequest, Result<DeleteModuleResponse>>
{
    private readonly IUnitToWork _unitToWork;

    public DeleteModuleRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result<DeleteModuleResponse>> Handle(DeleteModuleRequest request, CancellationToken cancellationToken)
    {
        var module = await _unitToWork.Modules.GetAsync(x => x.Id == request.Request.ModuleId, cancellationToken);

        if (module == null || module.UserId != request.UserId)
        {
            return new Result<DeleteModuleResponse>
            {
                Error = "Module not found"
            };
        }

        _unitToWork.Modules.Remove(module);

        await _unitToWork.CommitAsync();

        return new Result<DeleteModuleResponse>
        {
            Value = new DeleteModuleResponse
            {
                ModuleId = module.Id
            },
        };
    }
}

public class DeleteModuleRequest : IRequest<Result<DeleteModuleResponse>>
{
    public int UserId { get; set; }
    public DeleteModuleExternalRequest Request { get; set; } = null!;
}

public class DeleteModuleExternalRequest
{
    [FromRoute(Name = "moduleId")] public int ModuleId { get; set; }
}

public class DeleteModuleResponse
{
    public int ModuleId { get; set; }
}