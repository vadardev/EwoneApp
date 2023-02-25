using Common;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Domain.Requests.Card;

public class DeleteCardRequestHandler : IRequestHandler<DeleteCardRequest, Result>
{
    private readonly IUnitToWork _unitToWork;

    public DeleteCardRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result> Handle(DeleteCardRequest request, CancellationToken cancellationToken)
    {
        var card = await _unitToWork.Cards.GetAsync(x => x.Id == request.Request.CardId, cancellationToken);

        if (card != null && card.AuthorId == request.UserId)
        {
            card.ModuleId = null;

            await _unitToWork.CommitAsync();

            return new Result();
        }

        return new Result
        {
            Error = "Delete error",
        };
    }
}

public class DeleteCardExternalRequest
{
    [FromRoute(Name = "cardId")] public int CardId { get; set; }
}

public class DeleteCardRequest : IRequest<Result>
{
    public int UserId { get; set; }
    public DeleteCardExternalRequest Request { get; set; } = null!;
}