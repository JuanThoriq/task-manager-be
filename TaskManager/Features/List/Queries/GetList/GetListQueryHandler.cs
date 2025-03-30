using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Models.Response;

namespace TaskManager.Features.List.Queries.GetList
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, ListResponse?>
    {
        private readonly TaskManagerContext _db;

        public GetListQueryHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<ListResponse?> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var listEntity = await _db.Lists.FirstOrDefaultAsync(l => l.Id == request.ListId, cancellationToken);
            if (listEntity == null)
            {
                return null;
            }
            return new ListResponse
            {
                ListId = listEntity.Id,
                Title = listEntity.Title,
                Order = listEntity.Order,
                BoardId = listEntity.BoardId,
                CreatedAt = listEntity.CreatedAt,
                UpdatedAt = listEntity.UpdatedAt
            };
        }
    }
}
