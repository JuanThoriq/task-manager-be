using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Features.Board.Commands.DeleteBoard
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Unit>
    {
        private readonly TaskManagerContext _db;

        public DeleteBoardCommandHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(DeleteBoardCommand command, CancellationToken cancellationToken)
        {
            var board = await _db.Boards.FirstOrDefaultAsync(b => b.Id == command.BoardId, cancellationToken);
            if (board == null)
            {
                throw new Exception("Board not found.");
            }
            _db.Boards.Remove(board);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
