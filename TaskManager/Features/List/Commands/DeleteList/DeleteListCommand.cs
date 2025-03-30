using MediatR;
using System;

namespace TaskManager.Features.List.Commands.DeleteList
{
    public class DeleteListCommand : IRequest<Unit>
    {
        public Guid ListId { get; set; }
        public DeleteListCommand(Guid listId)
        {
            ListId = listId;
        }
    }
}
