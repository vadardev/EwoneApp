using Common;
using Ewone.Data.Entities;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Domain.Requests.Card;

public class EditCardRequestHandler : IRequestHandler<EditCardRequest, Result<EditCardResponse>>
{
    private readonly IUnitToWork _unitToWork;

    public EditCardRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result<EditCardResponse>> Handle(EditCardRequest request, CancellationToken cancellationToken)
    {
        var card = await _unitToWork.Cards.GetAsync(x => x.Id == request.Request.CardId, cancellationToken);

        if (card != null && card.AuthorId == request.UserId)
        {
            var word = await _unitToWork.Words.GetAsync(x => x.Name == request.Request.Word.ToLower(),
                cancellationToken);

            if (word == null)
            {
                word = new Word
                {
                    CreateDate = DateTime.UtcNow,
                    Name = request.Request.Word.ToLower(),
                };

                await _unitToWork.Words.AddAsync(word, cancellationToken);
            }

            card.Word = word;
            card.ImageUrl = request.Request.ImageUrl;
            card.Definition = request.Request.Definition;
            card.Examples = request.Request.Examples.ToList();

            await _unitToWork.CommitAsync();
        }

        return new Result<EditCardResponse>
        {
            Value = new EditCardResponse
            {
                CardId = request.Request.CardId,
            },
        };
    }
}

public class EditCardExternalRequest
{
    [FromRoute(Name = "cardId")] public int CardId { get; set; }
    public string Word { get; set; } = null!;
    public string Definition { get; set; } = null!;
    public IEnumerable<string> Examples { get; set; } = null!;
    public string? ImageUrl { get; set; }
}

public class EditCardRequest : IRequest<Result<EditCardResponse>>
{
    public int UserId { get; set; }
    public EditCardExternalRequest Request { get; set; } = null!;
}

public class EditCardResponse
{
    public int CardId { get; set; }
}