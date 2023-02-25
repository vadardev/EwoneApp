using Common;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Domain.Requests.Card;

public class GetCardsRequestHandler : IRequestHandler<GetCardsRequest, Result<GetCardsResponse>>
{
    private readonly IUnitToWork _unitToWork;

    public GetCardsRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result<GetCardsResponse>> Handle(GetCardsRequest request, CancellationToken cancellationToken)
    {
        var cards = await _unitToWork.Cards.GetAllAsync(x => x.ModuleId == request.Request.ModuleId, cancellationToken);

        return new Result<GetCardsResponse>
        {
            Value = new GetCardsResponse
            {
                Cards = cards.Select(x => new CardItem
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

public class GetCardsExternalRequest
{
    [FromRoute] public int ModuleId { get; set; }
}

public class GetCardsRequest : IRequest<Result<GetCardsResponse>>
{
    public int UserId { get; set; }
    public GetCardsExternalRequest Request { get; set; } = null!;
}

public class GetCardsResponse
{
    public IEnumerable<CardItem> Cards { get; set; }
}

public class CardItem
{
    public int CardId { get; set; }
    public string Word { get; set; } = null!;
    public string Defition { get; set; } = null!;
    public string? ImageUrl { get; set; }
}