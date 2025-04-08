using MediatR;
using TaskManager.Entities;
using TaskManager.Models.Response;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Features.List.Commands.CreateList
{
    public class CreateListCommandHandler : IRequestHandler<CreateListCommand, ListResponse>
    {
        private readonly TaskManagerContext _db;

        public CreateListCommandHandler(TaskManagerContext db)
        {
            _db = db;
        }

        public async Task<ListResponse> Handle(CreateListCommand command, CancellationToken cancellationToken)
        {
            // Ambil nilai maximum order pada board yang sama; jika tidak ada, gunakan default 0.
            var maxOrder = await _db.Lists
                .Where(l => l.BoardId == command.BoardId)
                .MaxAsync(l => (int?)l.Order, cancellationToken) ?? 0;

            // Set nilai order untuk list baru menjadi maxOrder + 1
            command.Order = maxOrder + 1;

            var listEntity = new TaskManager.Entities.List
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Order = command.Order,
                BoardId = command.BoardId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Lists.Add(listEntity);
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
