using MediatR;
using TaskManager.Entities;
using TaskManager.Models.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

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
