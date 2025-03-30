using MediatR;
using TaskManager.Models.Response;
using System;
using System.Collections.Generic;

namespace TaskManager.Features.Card.Queries.GetCardsByLIst
{
    public class GetCardsByListQuery : IRequest<List<CardResponse>>
    {
        public Guid ListId { get; set; }
        public GetCardsByListQuery(Guid listId)
        {
            ListId = listId;
        }
    }
}
