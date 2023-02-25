using Common;
using Ewone.Domain.Requests.Card;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ewone.Api.Controllers;

[Authorize]
[ApiController]
[Route("cards")]
public class CardController : BaseController
{
    private readonly IMediator _mediator;

    public CardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<Result<GetCardsResponse>> GetCards([FromRoute] GetCardsExternalRequest request)
    {
        return _mediator.Send(new GetCardsRequest
        {
            UserId = GetUserId(),
            Request = request,
        });
    }

    [HttpPost("add")]
    public Task<Result<AddCardResponse>> AddCard([FromBody] AddCardExternalRequest request)
    {
        return _mediator.Send(new AddCardRequest
        {
            UserId = GetUserId(),
            Body = request,
        });
    }

    [HttpDelete("{cardId}")]
    public Task<Result> DeleteCard([FromRoute] DeleteCardExternalRequest request)
    {
        return _mediator.Send(new DeleteCardRequest
        {
            UserId = GetUserId(),
            Request = request,
        });
    }

    [HttpPatch("{cardId}")]
    public Task<Result<EditCardResponse>> EditCard([FromBody] EditCardExternalRequest request)
    {
        return _mediator.Send(new EditCardRequest
        {
            UserId = GetUserId(),
            Request = request,
        });
    }
}