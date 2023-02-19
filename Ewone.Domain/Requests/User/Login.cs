using Ewone.Data.Entities;
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

            var module = new Data.Entities.Module
            {
                CreateDate = DateTime.UtcNow,
                Name = "Default",
                User = user,
            };

            await _unitToWork.Modules.AddAsync(module, cancellationToken);

            IEnumerable<Card> defaultCards = await _unitToWork.Cards.GetAllAsync(x => x.ModuleId == 0, cancellationToken);

            foreach (var card in defaultCards)
            {
                await _unitToWork.Cards.AddAsync(new Card
                {
                    CreateDate = DateTime.UtcNow,
                    WordId = 0,
                    Word = card.Word,
                    Definition = card.Definition,
                    ImageUrl = card.ImageUrl,
                    Examples = card.Examples,
                    Parent = card,
                    Author = user,
                    Module = module,
                }, cancellationToken);
            }

            await _unitToWork.CommitAsync();
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