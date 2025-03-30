using MediatR;
using TaskManager.Entities;
using TaskManager.Models.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Features.Card.Commands.CreateCard
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, CardResponse>
    {
        private readonly TaskManagerContext _db;

        public CreateCardCommandHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<CardResponse> Handle(CreateCardCommand command, CancellationToken cancellationToken)
        {
            var cardEntity = new TaskManager.Entities.Card
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Order = command.Order,
                Description = command.Description,
                ListId = command.ListId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Cards.Add(cardEntity);
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
