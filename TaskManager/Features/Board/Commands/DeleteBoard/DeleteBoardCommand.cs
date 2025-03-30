using MediatR;
using System;
using TaskManager.Models.Response;

namespace TaskManager.Features.Board.Commands.DeleteBoard
{
    public class DeleteBoardCommand : IRequest<Unit>
    {
        public Guid BoardId { get; set; }
        public DeleteBoardCommand(Guid boardId)
        {
            BoardId = boardId;
        }
    }
}
