using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Models.Response;

namespace TaskManager.Features.Board.Queries.GetBoards
{
    public class GetBoardsQueryHandler : IRequestHandler<GetBoardsQuery, List<BoardResponse>>
    {
        private readonly TaskManagerContext _db;

        public GetBoardsQueryHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<List<BoardResponse>> Handle(GetBoardsQuery request, CancellationToken cancellationToken)
        {
            var boards = await _db.Boards
                .Where(b => b.OrgId == request.OrgId)
                .ToListAsync(cancellationToken);

            return boards.Select(b => new BoardResponse
            {
                BoardId = b.Id,
                OrgId = b.OrgId,
                Title = b.Title,
                ImageId = b.ImageId,
                ImageThumbUrl = b.ImageThumbUrl,
                ImageFullUrl = b.ImageFullUrl,
                ImageUserName = b.ImageUserName,
                ImageLinkHtml = b.ImageLinkHtml
            }).ToList();
        }
    }
}
