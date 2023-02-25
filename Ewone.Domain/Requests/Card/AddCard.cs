using Common;
using Ewone.Data.Entities;
using Ewone.Data.Repositories.UnitToWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Domain.Requests.Card;

public class AddCardRequestHandler : IRequestHandler<AddCardRequest, Result<AddCardResponse>>
{
    private readonly IUnitToWork _unitToWork;

    public AddCardRequestHandler(IUnitToWork unitToWork)
    {
        _unitToWork = unitToWork;
    }

    public async Task<Result<AddCardResponse>> Handle(AddCardRequest request, CancellationToken cancellationToken)
    {
        var word = await _unitToWork.Words.GetAsync(x => x.Name == request.Body.Word.ToLower(), cancellationToken);

        if (word == null)
        {
            word = new Word
            {
                CreateDate = DateTime.UtcNow,
                Name = request.Body.Word.ToLower(),
            };

            await _unitToWork.Words.AddAsync(word, cancellationToken);
        }

        var card = new Data.Entities.Card
        {
            CreateDate = DateTime.UtcNow,
            Word = word,
            Definition = request.Body.Definition,
            ImageUrl = request.Body.ImageUrl,
            Examples = request.Body.Examples.ToList(),
            AuthorId = request.UserId,
            ModuleId = request.Body.ModuleId,
        };
        await _unitToWork.Cards.AddAsync(card, cancellationToken);

        await _unitToWork.CommitAsync();

        return new Result<AddCardResponse>
        {
            Value = new AddCardResponse
            {
                CardId = card.Id,
            },
        };
    }
}

public class AddCardExternalRequest
{
    public int ModuleId { get; set; }
    public string Word { get; set; } = null!;
    public string Definition { get; set; } = null!;
    public IEnumerable<string> Examples { get; set; } = null!;
    public string? ImageUrl { get; set; }
}

public class AddCardRequest : IRequest<Result<AddCardResponse>>
{
    public int UserId { get; set; }
    public AddCardExternalRequest Body { get; set; } = null!;
}

public class AddCardResponse
{
    public int CardId { get; set; }
}