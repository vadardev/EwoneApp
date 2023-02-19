using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;

namespace Ewone.Domain.Requests.Module;

public class GetDefaultModuleRequestHandler : IRequestHandler<GetDefaultModuleRequest, Result<GetDefaultModuleResponse>>
{
    private readonly IUnitToWork _unitToWork;

    public GetDefaultModuleRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result<GetDefaultModuleResponse>> Handle(GetDefaultModuleRequest request, CancellationToken cancellationToken)
    {
        Data.Entities.Module? module = await _unitToWork.Modules.GetAsync(x => x.Id == 1, cancellationToken);

        if (module == null)
        {
            return new Result<GetDefaultModuleResponse>
            {
                Error = "Module not found"
            };
        }

        return new Result<GetDefaultModuleResponse>
        {
            Value = new GetDefaultModuleResponse
            {
                Id = module.Id,
                Name = module.Name,
            }
        };
    }
}

public class GetDefaultModuleRequest : IRequest<Result<GetDefaultModuleResponse>>
{
   
}

public class GetDefaultModuleResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}