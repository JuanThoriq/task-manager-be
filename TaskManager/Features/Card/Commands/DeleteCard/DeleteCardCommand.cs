using MediatR;
using System;

namespace TaskManager.Features.Card.Commands.DeleteCard
{
    public class DeleteCardCommand : IRequest<Unit>
    {
        public Guid CardId { get; set; }
        public DeleteCardCommand(Guid cardId)
        {
            CardId = cardId;
        }
    }
}
