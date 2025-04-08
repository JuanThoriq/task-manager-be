using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.Board.Commands.CreateBoard
{
    public class CreateBoardCommand : IRequest<BoardResponse>
    {
        public string OrgId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
