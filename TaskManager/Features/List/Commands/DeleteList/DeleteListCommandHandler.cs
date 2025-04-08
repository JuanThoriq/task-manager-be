using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Features.List.Commands.DeleteList
{
    public class DeleteListCommandHandler : IRequestHandler<DeleteListCommand, Unit>
    {
        private readonly TaskManagerContext _db;

        public DeleteListCommandHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(DeleteListCommand command, CancellationToken cancellationToken)
        {
            var listEntity = await _db.Lists.FirstOrDefaultAsync(l => l.Id == command.ListId, cancellationToken);
            if (listEntity == null)
            {
                throw new Exception("List not found.");
            }

            // Simpan nilai order yang akan dihapus
            int deletedOrder = listEntity.Order;
            Guid boardId = listEntity.BoardId;

            _db.Lists.Remove(listEntity);

            // Update Order untuk card lain di dalam list yang sama yang memiliki order lebih tinggi
            var listsToUpdate = await _db.Lists
                .Where(l => l.BoardId == boardId && l.Order > deletedOrder)
                .ToListAsync(cancellationToken);

            foreach (var list in listsToUpdate)
            {
                list.Order -= 1;
            }

            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
