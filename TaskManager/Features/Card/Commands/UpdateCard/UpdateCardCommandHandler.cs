using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using TaskManager.Models.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Features.Card.Commands.UpdateCard
{
    public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, CardResponse>
    {
        private readonly TaskManagerContext _db;

        public UpdateCardCommandHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<CardResponse> Handle(UpdateCardCommand command, CancellationToken cancellationToken)
        {
            var cardEntity = await _db.Cards.FirstOrDefaultAsync(c => c.Id == command.CardId, cancellationToken);
            if (cardEntity == null)
            {
                throw new Exception("Card not found.");
            }

            cardEntity.Title = command.Title;
            cardEntity.Order = command.Order;
            cardEntity.Description = command.Description;
            cardEntity.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync(cancellationToken);

            return new CardResponse
            {
                CardId = cardEntity.Id,
                Title = cardEntity.Title,
                Order = cardEntity.Order,
                Description = cardEntity.Description,
                ListId = cardEntity.ListId,
                CreatedAt = cardEntity.CreatedAt,
                UpdatedAt = cardEntity.UpdatedAt
            };
        }
    }
}
