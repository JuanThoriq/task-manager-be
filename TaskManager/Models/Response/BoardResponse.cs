using System;

namespace TaskManager.Models.Response
{
    public class BoardResponse
    {
        public Guid BoardId { get; set; }
        public string OrgId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ImageId { get; set; } = string.Empty;
        public string ImageThumbUrl { get; set; } = string.Empty;
        public string ImageFullUrl { get; set; } = string.Empty;
        public string ImageUserName { get; set; } = string.Empty;
        public string ImageLinkHtml { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
