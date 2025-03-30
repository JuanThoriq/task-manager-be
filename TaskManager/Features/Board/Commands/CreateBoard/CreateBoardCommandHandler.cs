using MediatR;
using TaskManager.Entities;
using TaskManager.Models.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Features.Board.Commands.CreateBoard
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardResponse>
    {
        private readonly TaskManagerContext _db;

        public CreateBoardCommandHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<BoardResponse> Handle(CreateBoardCommand command, CancellationToken cancellationToken)
        {
            var board = new Entities.Board
            {
                Id = Guid.NewGuid(),
                OrgId = command.OrgId,
                Title = command.Title,
                ImageId = command.ImageId,
                ImageThumbUrl = command.ImageThumbUrl,
                ImageFullUrl = command.ImageFullUrl,
                ImageUserName = command.ImageUserName,
                ImageLinkHtml = command.ImageLinkHtml,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Boards.Add(board);
            await _db.SaveChangesAsync(cancellationToken);

            return new BoardResponse
            {
                BoardId = board.Id,
                OrgId = board.OrgId,
                Title = board.Title,
                ImageId = board.ImageId,
                ImageThumbUrl = board.ImageThumbUrl,
                ImageFullUrl = board.ImageFullUrl,
                ImageUserName = board.ImageUserName,
                ImageLinkHtml = board.ImageLinkHtml,
                CreatedAt = board.CreatedAt,
                UpdatedAt = board.UpdatedAt
            };
        }
    }
}
