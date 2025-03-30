using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Features.List.Commands.CreateList;
using TaskManager.Features.List.Commands.DeleteList;
using TaskManager.Features.List.Commands.UpdateList;
using TaskManager.Features.List.Queries.GetList;
using TaskManager.Features.List.Queries.GetListsByBoard;
using TaskManager.Models.Response;

namespace TaskManager.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ListController> _logger;

        public ListController(IMediator mediator, ILogger<ListController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: /api/boards/{boardId}/lists
        [HttpGet("boards/{boardId:guid}/lists")]
        public async Task<IActionResult> GetListsByBoard(Guid boardId)
        {
            _logger.LogInformation("Request to get lists for board {boardId}", boardId);
            try
            {
                var query = new GetListsByBoardQuery(boardId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving lists for board {boardId}", boardId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // GET: /api/lists/{listId}
        [HttpGet("lists/{listId:guid}")]
        public async Task<IActionResult> GetList(Guid listId)
        {
            _logger.LogInformation("Request to get list with ID {listId}", listId);
            try
            {
                var query = new GetListQuery(listId);
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving list with ID {listId}", listId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // POST: /api/boards/{boardId}/lists
        [HttpPost("boards/{boardId:guid}/lists")]
        public async Task<IActionResult> CreateList(Guid boardId, [FromBody] CreateListCommand command)
        {
            _logger.LogInformation("Request to create list for board {boardId} with data: {@command}", boardId, command);
            try
            {
                // Pastikan command tahu boardId dari route
                command.BoardId = boardId;
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetList), new { listId = result.ListId }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating list for board {boardId}", boardId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // PUT: /api/lists/{listId}
        [HttpPut("lists/{listId:guid}")]
        public async Task<IActionResult> UpdateList(Guid listId, [FromBody] UpdateListCommand command)
        {
            _logger.LogInformation("Request to update list {listId} with data: {@command}", listId, command);
            try
            {
                // Pastikan command memiliki listId dari route
                command.ListId = listId;
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating list {listId}", listId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // DELETE: /api/lists/{listId}
        [HttpDelete("lists/{listId:guid}")]
        public async Task<IActionResult> DeleteList(Guid listId)
        {
            _logger.LogInformation("Request to delete list {listId}", listId);
            try
            {
                var command = new DeleteListCommand(listId);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting list {listId}", listId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }
    }
}
