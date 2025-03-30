using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Models.Response;

namespace TaskManager.Features.Card.Queries.GetCard
{
    public class GetCardQueryHandler : IRequestHandler<GetCardQuery, CardResponse?>
    {
        private readonly TaskManagerContext _db;

        public GetCardQueryHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<CardResponse?> Handle(GetCardQuery request, CancellationToken cancellationToken)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(c => c.Id == request.CardId, cancellationToken);
            if (card == null)
            {
                return null;
            }
            return new CardResponse
            {
                CardId = card.Id,
                Title = card.Title,
                Order = card.Order,
                Description = card.Description,
                ListId = card.ListId,
                CreatedAt = card.CreatedAt,
                UpdatedAt = card.UpdatedAt
            };
        }
    }
}
