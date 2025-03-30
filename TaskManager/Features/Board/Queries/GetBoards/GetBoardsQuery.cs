using MediatR;
using System.Collections.Generic;
using TaskManager.Models.Response;

namespace TaskManager.Features.Board.Queries.GetBoards
{
    public class GetBoardsQuery : IRequest<List<BoardResponse>>
    {
        public string OrgId { get; set; } = string.Empty;
        public GetBoardsQuery(string orgId)
        {
            OrgId = orgId;
        }
    }
}
