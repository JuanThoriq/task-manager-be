using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.Card.Commands.CreateCard
{
    public class CreateCardCommand : IRequest<CardResponse>
    {
        public Guid ListId { get; set; }  // Akan di-set dari route
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? Description { get; set; }  // Optional
    }
}
