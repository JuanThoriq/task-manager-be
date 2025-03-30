using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Features.Board.Commands.CreateBoard;
using TaskManager.Features.Board.Commands.DeleteBoard;
using TaskManager.Features.Board.Commands.UpdateBoard;
using TaskManager.Features.Board.Queries.GetBoard;
using TaskManager.Features.Board.Queries.GetBoards;
using TaskManager.Models.Response;

namespace TaskManager.Controllers
{
    [Route("api/v1/boards")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BoardController> _logger;

        public BoardController(IMediator mediator, ILogger<BoardController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: /api/v1/boards
        [HttpGet]
        public async Task<IActionResult> GetBoards([FromQuery] string orgId)
        {
            _logger.LogInformation("Request to get boards for OrgId: {orgId}", orgId);
            try
            {
                var query = new GetBoardsQuery(orgId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving boards for OrgId: {orgId}", orgId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // GET: /api/v1/boards/{boardId}
        [HttpGet("{boardId:guid}")]
        public async Task<IActionResult> GetBoard(Guid boardId)
        {
            _logger.LogInformation("Request to get board with ID: {boardId}", boardId);
            try
            {
                var query = new GetBoardQuery(boardId);
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving board with ID: {boardId}", boardId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // POST: /api/v1/boards
        [HttpPost]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardCommand command)
        {
            _logger.LogInformation("Request to create board: {@command}", command);
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetBoard), new { boardId = result.BoardId }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating board");
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // PUT: /api/v1/boards/{boardId}
        [HttpPut("{boardId:guid}")]
        public async Task<IActionResult> UpdateBoard(Guid boardId, [FromBody] UpdateBoardCommand command)
        {
            _logger.LogInformation("Request to update board with ID: {boardId} and data: {@command}", boardId, command);
            try
            {
                // Pastikan command memiliki boardId yang sesuai
                command.BoardId = boardId;
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating board with ID: {boardId}", boardId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }

        // DELETE: /api/v1/boards/{boardId}
        [HttpDelete("{boardId:guid}")]
        public async Task<IActionResult> DeleteBoard(Guid boardId)
        {
            _logger.LogInformation("Request to delete board with ID: {boardId}", boardId);
            try
            {
                var command = new DeleteBoardCommand(boardId);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting board with ID: {boardId}", boardId);
                return Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: 500);
            }
        }
    }
}
