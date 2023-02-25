using Common;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;

namespace Ewone.Domain.Requests.Module;

public class GetModulesRequestHandler : IRequestHandler<GetModulesRequest, Result<GetModulesResponse>>
{
    private readonly IUnitToWork _unitToWork;

    public GetModulesRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result<GetModulesResponse>> Handle(GetModulesRequest request, CancellationToken cancellationToken)
    {
        var modules = await _unitToWork.Modules.GetAllAsync(x => x.UserId == request.UserId, cancellationToken);

        return new Result<GetModulesResponse>
        {
            Value = new GetModulesResponse
            {
                Modules = modules.Select(x => new ModuleItem
                {
                    Id = x.Id,
                    Name = x.Name,
                })
            },
        };
    }
}

public class GetModulesRequest :  IRequest<Result<GetModulesResponse>>
{
    public int UserId { get; set; }
}

public class GetModulesResponse
{
    public IEnumerable<ModuleItem> Modules { get; set; } = null!;
}

public class ModuleItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

