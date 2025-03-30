using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Models.Response;

namespace TaskManager.Features.List.Queries.GetListsByBoard
{
    public class GetListsByBoardQueryHandler : IRequestHandler<GetListsByBoardQuery, List<ListResponse>>
    {
        private readonly TaskManagerContext _db;

        public GetListsByBoardQueryHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<List<ListResponse>> Handle(GetListsByBoardQuery request, CancellationToken cancellationToken)
        {
            var lists = await _db.Lists
                .Where(l => l.BoardId == request.BoardId)
                .ToListAsync(cancellationToken);

            return lists.Select(l => new ListResponse
            {
                ListId = l.Id,
                Title = l.Title,
                Order = l.Order,
                BoardId = l.BoardId,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            }).ToList();
        }
    }
}
