using Ewone.Data.Repositories.UnitToWork;
using MediatR;

namespace Ewone.Api.Requests.User;

public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IUnitToWork _unitToWork;

    public GetUsersRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        return new GetUsersResponse
        {
            Emails = (await _unitToWork.Users.GetAllAsync(cancellationToken)).Select(x => x.Email),
        };
    }
}

public class GetUsersRequest : IRequest<GetUsersResponse>
{
}

public class GetUsersResponse
{
    public IEnumerable<string> Emails { get; set; } = null!;
}