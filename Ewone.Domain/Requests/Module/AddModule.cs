using Common;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Domain.Requests.Module;

public class AddModuleRequestHandler : IRequestHandler<AddModuleRequest, Result<AddModuleResponse>>
{
    private readonly IUnitToWork _unitToWork;

    public AddModuleRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result<AddModuleResponse>> Handle(AddModuleRequest request, CancellationToken cancellationToken)
    {
        var module = new Data.Entities.Module
        {
            CreateDate = default,
            Name = request.Request.Body.Name,
            Description = request.Request.Body.Description,
            UserId = request.UserId,
        };

        await _unitToWork.Modules.AddAsync(module, cancellationToken);

        await _unitToWork.CommitAsync();

        return new Result<AddModuleResponse>
        {
            Value = new AddModuleResponse
            {
                ModuleId = module.Id,
            },
        };
    }
}

public class AddModuleExternalRequest
{
    [FromBody]
    public AddModuleBody Body { get; set; } = null!;
}

public class AddModuleBody
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class AddModuleRequest : IRequest<Result<AddModuleResponse>>
{
    public int UserId { get; set; }
    public AddModuleExternalRequest Request { get; set; } = null!;
}

public class AddModuleResponse
{
    public int ModuleId { get; set; }
}