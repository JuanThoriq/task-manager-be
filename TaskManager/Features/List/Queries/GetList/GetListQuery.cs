using MediatR;
using TaskManager.Models.Response;
using System;

namespace TaskManager.Features.List.Queries.GetList
{
    public class GetListQuery : IRequest<ListResponse?>
    {
        public Guid ListId { get; set; }
        public GetListQuery(Guid listId)
        {
            ListId = listId;
        }
    }
}
