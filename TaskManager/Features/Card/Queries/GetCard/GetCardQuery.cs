using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.Card.Queries.GetCard
{
    public class GetCardQuery : IRequest<CardResponse?>
    {
        public Guid CardId { get; set; }
        public GetCardQuery(Guid cardId)
        {
            CardId = cardId;
        }
    }
}
