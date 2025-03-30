using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.Board.Queries.GetBoard
{
    public class GetBoardQuery : IRequest<BoardResponse?>
    {
        public Guid BoardId { get; set; }
        public GetBoardQuery(Guid boardId)
        {
            BoardId = boardId;
        }
    }
}
