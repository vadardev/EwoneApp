using Common;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;

namespace Ewone.Domain.Requests.Card;

public class GetDefaultCardsRequestHandler : IRequestHandler<GetDefaultCardsRequest, Result<GetDefaultCardsResponse>>
{
    private readonly IUnitToWork _unitToWork;

    public GetDefaultCardsRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result<GetDefaultCardsResponse>> Handle(GetDefaultCardsRequest request,
        CancellationToken cancellationToken)
    {
        IEnumerable<Data.Entities.Card> defaultCards =
            await _unitToWork.Cards.GetAllAsync(x => x.ModuleId == Constants.DefaultModuleId, cancellationToken);

        return new Result<GetDefaultCardsResponse>
        {
            Value = new GetDefaultCardsResponse
            {
                Cards = defaultCards.Select(x => new DefaultCardItem
                {
                    CardId = x.Id,
                    Word = x.Word.Name,
                    Defition = x.Definition,
                    ImageUrl = x.ImageUrl,
                }),
            },
        };
    }
}

public class GetDefaultCardsRequest : IRequest<Result<GetDefaultCardsResponse>>
{
}

public class GetDefaultCardsResponse
{
    public IEnumerable<DefaultCardItem> Cards { get; set; } = null!;
}

public class DefaultCardItem
{
    public int CardId { get; set; }
    public string Word { get; set; } = null!;
    public string Defition { get; set; } = null!;
    public string? ImageUrl { get; set; }
}