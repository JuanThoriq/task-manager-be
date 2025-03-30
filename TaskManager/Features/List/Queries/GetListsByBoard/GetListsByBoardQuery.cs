using MediatR;
using TaskManager.Models.Response;
using System;
using System.Collections.Generic;

namespace TaskManager.Features.List.Queries.GetListsByBoard
{
    public class GetListsByBoardQuery : IRequest<List<ListResponse>>
    {
        public Guid BoardId { get; set; }
        public GetListsByBoardQuery(Guid boardId)
        {
            BoardId = boardId;
        }
    }
}
