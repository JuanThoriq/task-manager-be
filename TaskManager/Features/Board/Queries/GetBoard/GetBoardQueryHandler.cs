using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Models.Response;

namespace TaskManager.Features.Board.Queries.GetBoard
{
    public class GetBoardQueryHandler : IRequestHandler<GetBoardQuery, BoardResponse?>
    {
        private readonly TaskManagerContext _db;

        public GetBoardQueryHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<BoardResponse?> Handle(GetBoardQuery request, CancellationToken cancellationToken)
        {
            var board = await _db.Boards.FirstOrDefaultAsync(b => b.Id == request.BoardId, cancellationToken);
            if (board == null)
            {
                return null;
            }
            return new BoardResponse
            {
                BoardId = board.Id,
                OrgId = board.OrgId,
                Title = board.Title,
                ImageId = board.ImageId,
                ImageThumbUrl = board.ImageThumbUrl,
                ImageFullUrl = board.ImageFullUrl,
                ImageUserName = board.ImageUserName,
                ImageLinkHtml = board.ImageLinkHtml
            };
        }
    }
}
