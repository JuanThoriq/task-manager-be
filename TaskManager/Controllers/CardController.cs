using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Features.Board.Commands.CreateBoard;
using TaskManager.Features.Board.Commands.DeleteBoard;
using TaskManager.Features.Board.Commands.UpdateBoard;
using TaskManager.Features.Board.Queries.GetBoard;
using TaskManager.Features.Card.Commands.CreateCard;
using TaskManager.Features.Card.Commands.DeleteCard;
using TaskManager.Features.Card.Commands.UpdateCard;
using TaskManager.Features.Card.Queries.GetCard;
using TaskManager.Features.Card.Queries.GetCardsByLIst;
using TaskManager.Models.Response;

namespace TaskManager.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CardController> _logger;

        public CardController(IMediator mediator, ILogger<CardController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: /api/lists/{listId}/cards
        [HttpGet("lists/{listId:guid}/cards")]
        public async Task<IActionResult> GetCardsByList(Guid listId)
        {
            _logger.LogInformation("Request to get cards for list {listId}", listId);
            try
            {
                var query = new GetCardsByListQuery(listId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cards for list {listId}", listId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // GET: /api/cards/{cardId}
        [HttpGet("cards/{cardId:guid}")]
        public async Task<IActionResult> GetCard(Guid cardId)
        {
            _logger.LogInformation("Request to get card with ID {cardId}", cardId);
            try
            {
                var query = new GetCardQuery(cardId);
                var result = await _mediator.Send(query);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving card with ID {cardId}", cardId);
                return Problem(
                    title: "Internal Server Error",
                detail: ex.Message,
                    statusCode: 500);
            }
        }

        // POST: /api/lists/{listId}/cards
        [HttpPost("lists/{listId:guid}/cards")]
        public async Task<IActionResult> CreateCard(Guid listId, [FromBody] CreateCardCommand command)
        {
            _logger.LogInformation("Request to create card for list {listId} with data: {@command}", listId, command);
            try
            {
                command.ListId = listId; // Set board listId dari route
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetCard), new { cardId = result.CardId }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating card for list {listId}", listId);
                return Problem(
                    title: "Internal Server Error",
                detail: ex.Message,
                    statusCode: 500);
            }
        }

        // PUT: /api/cards/{cardId}
        [HttpPut("cards/{cardId:guid}")]
        public async Task<IActionResult> UpdateCard(Guid cardId, [FromBody] UpdateCardCommand command)
        {
            _logger.LogInformation("Request to update card {cardId} with data: {@command}", cardId, command);
            try
            {
                command.CardId = cardId;
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating card {cardId}", cardId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // DELETE: /api/cards/{cardId}
        [HttpDelete("cards/{cardId:guid}")]
        public async Task<IActionResult> DeleteCard(Guid cardId)
        {
            _logger.LogInformation("Request to delete card {cardId}", cardId);
            try
            {
                var command = new DeleteCardCommand(cardId);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting card {cardId}", cardId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }
    }
}
