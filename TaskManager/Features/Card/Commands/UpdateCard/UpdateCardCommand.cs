using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.Card.Commands.UpdateCard
{
    public class UpdateCardCommand : IRequest<CardResponse>
    {
        public Guid CardId { get; set; }  // Di-set dari route
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? Description { get; set; }
    }
}
