using MediatR;
using TaskManager.Entities;
using TaskManager.Models.Response;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Features.List.Commands.CreateList;

namespace TaskManager.Features.Board.Commands.CreateBoard
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardResponse>
    {
        private readonly TaskManagerContext _db;
        private readonly IMediator _mediator;

        public CreateBoardCommandHandler(TaskManagerContext db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        public async Task<BoardResponse> Handle(CreateBoardCommand command, CancellationToken cancellationToken)
        {
            var board = new Entities.Board
            {
                Id = Guid.NewGuid(),
                OrgId = command.OrgId,
                Title = command.Title,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Boards.Add(board);
            await _db.SaveChangesAsync(cancellationToken);

            // Setelah board berhasil dibuat, buat default list
            var defaultListTitles = new[] { "To Do", "In Progress", "Done" };

            foreach (var title in defaultListTitles)
            {
                // Misalnya, buat command baru untuk pembuatan list default.
                var createListCmd = new CreateListCommand
                {
                    BoardId = board.Id,
                    Title = title
                    // Order tidak disuplai dari input, handler CreateList sendiri akan menghitung Order.
                };
                // Anda dapat menggunakan mediator untuk mengirim perintah tersebut.
                await _mediator.Send(createListCmd, cancellationToken);
            }

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
