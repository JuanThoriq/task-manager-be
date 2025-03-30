using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Models.Response;

namespace TaskManager.Features.Card.Queries.GetCardsByLIst
{
    public class GetCardsByListQueryHandler : IRequestHandler<GetCardsByListQuery, List<CardResponse>>
    {
        private readonly TaskManagerContext _db;

        public GetCardsByListQueryHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<List<CardResponse>> Handle(GetCardsByListQuery request, CancellationToken cancellationToken)
        {
            var cards = await _db.Cards
                .Where(c => c.ListId == request.ListId)
                .ToListAsync(cancellationToken);

            return cards.Select(c => new CardResponse
            {
                CardId = c.Id,
                Title = c.Title,
                Order = c.Order,
                Description = c.Description,
                ListId = c.ListId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }
    }
}
