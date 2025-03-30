using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.Board.Commands.CreateBoard
{
    public class CreateBoardCommand : IRequest<BoardResponse>
    {
        public string OrgId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ImageId { get; set; } = string.Empty;
        public string ImageThumbUrl { get; set; } = string.Empty;
        public string ImageFullUrl { get; set; } = string.Empty;
        public string ImageUserName { get; set; } = string.Empty;
        public string ImageLinkHtml { get; set; } = string.Empty;
    }
}
