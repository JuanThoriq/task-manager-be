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

            _db.Lists.Remove(listEntity);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
