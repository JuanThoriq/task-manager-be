using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.Board.Commands.UpdateBoard
{
    public class UpdateBoardCommand : IRequest<BoardResponse>
    {
        // BoardId akan diisi dari parameter route oleh controller
        public Guid BoardId { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
