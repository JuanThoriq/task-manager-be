using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Models.Response;

namespace TaskManager.Features.Board.Commands.UpdateBoard
{
    public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, BoardResponse>
    {
        private readonly TaskManagerContext _db;

        public UpdateBoardCommandHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<BoardResponse> Handle(UpdateBoardCommand command, CancellationToken cancellationToken)
        {
            var board = await _db.Boards.FirstOrDefaultAsync(b => b.Id == command.BoardId, cancellationToken);
            if (board == null)
            {
                // Anda dapat lempar exception atau mengembalikan NotFound melalui mekanisme global
                throw new Exception("Board not found.");
            }

            // Update properti board
            board.Title = command.Title;
            board.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync(cancellationToken);

            return new BoardResponse
            {
                BoardId = board.Id,
                OrgId = board.OrgId,
                Title = board.Title,
                CreatedAt = board.CreatedAt,
                UpdatedAt = board.UpdatedAt
            };
        }
    }
}
