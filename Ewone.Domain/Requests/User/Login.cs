using Ewone.Data.Repositories.UnitToWork;
using Ewone.Domain.Helpers.Jwt;
using MediatR;
using UserEntity = Ewone.Data.Entities.User;

namespace Ewone.Domain.Requests.User;

public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUnitToWork _unitToWork;
    private readonly IJwtHelper _jwtHelper;

    public LoginRequestHandler(
        IUnitToWork unitToWork,
        IJwtHelper jwtHelper
    )
    {
        _unitToWork = unitToWork;
        _jwtHelper = jwtHelper;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var payload = await _jwtHelper.VerifyGoogleToken(request.IdToken);
        if (payload == null)
            throw new Exception("Invalid External Authentication.");
        ;
        UserEntity? user = await _unitToWork.Users.GetAsync(x => x.Email == payload.Email, cancellationToken);

        if (user == null)
        {
            user = new UserEntity
            {
                Email = payload.Email,
                Name = payload.Name,
                CreateDate = DateTime.UtcNow,
            };

            await _unitToWork.Users.AddAsync(user, cancellationToken);

            // IEnumerable<CardWordView> cards = await _cardRepository.GetDefaultCards();
            //
            // foreach (var card in cards)
            // {
            //     await _userCardRepository.Add(new UserCardEntity
            //     {
            //         Id = Guid.NewGuid(),
            //         IdUser = user.Id,
            //         IdCard = card.CardId,
            //     });
            // }
        }

        var token = _jwtHelper.CreateAccessToken(user);

        return new LoginResponse
        {
            Token = token, IsAuthSuccess = true,
        };
    }
}

public class LoginRequest : IRequest<LoginResponse>
{
    public string IdToken { get; set; } = null!;
}

public class LoginResponse
{
    public bool IsAuthSuccess { get; set; }
    public string Token { get; set; } = null!;
}