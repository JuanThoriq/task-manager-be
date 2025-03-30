using System;

namespace TaskManager.Models.Response
{
    public class ListResponse
    {
        public Guid ListId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public Guid BoardId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
