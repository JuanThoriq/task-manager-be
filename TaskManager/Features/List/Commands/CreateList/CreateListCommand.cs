using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.List.Commands.CreateList
{
    public class CreateListCommand : IRequest<ListResponse>
    {
        public Guid BoardId { get; set; }  // Akan di-set dari route oleh controller
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
