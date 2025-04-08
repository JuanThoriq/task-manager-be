using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Features.Card.Commands.DeleteCard
{
    public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand, Unit>
    {
        private readonly TaskManagerContext _db;

        public DeleteCardCommandHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var cardEntity = await _db.Cards.FirstOrDefaultAsync(c => c.Id == command.CardId, cancellationToken);
            if (cardEntity == null)
            {
                throw new Exception("Card not found.");
            }

            // Simpan nilai order yang akan dihapus
            int deletedOrder = cardEntity.Order;
            Guid listId = cardEntity.ListId;

            _db.Cards.Remove(cardEntity);

            // Update Order untuk card lain di dalam list yang sama yang memiliki order lebih tinggi
            var cardsToUpdate = await _db.Cards
                .Where(c => c.ListId == listId && c.Order > deletedOrder)
                .ToListAsync(cancellationToken);

            foreach (var card in cardsToUpdate)
            {
                card.Order -= 1;
            }

            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
