using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.List.Commands.UpdateList
{
    public class UpdateListCommand : IRequest<ListResponse>
    {
        public Guid ListId { get; set; }  // Di-set dari route
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
