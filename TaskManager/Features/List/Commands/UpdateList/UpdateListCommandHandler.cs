using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using TaskManager.Models.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Features.List.Commands.UpdateList
{
    public class UpdateListCommandHandler : IRequestHandler<UpdateListCommand, ListResponse>
    {
        private readonly TaskManagerContext _db;

        public UpdateListCommandHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<ListResponse> Handle(UpdateListCommand command, CancellationToken cancellationToken)
        {
            var listEntity = await _db.Lists.FirstOrDefaultAsync(l => l.Id == command.ListId, cancellationToken);
            if (listEntity == null)
            {
                throw new Exception("List not found.");
            }

            listEntity.Title = command.Title;
            listEntity.Order = command.Order;
            listEntity.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync(cancellationToken);

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
