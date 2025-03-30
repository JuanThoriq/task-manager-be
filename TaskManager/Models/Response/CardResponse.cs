using System;

namespace TaskManager.Models.Response
{
    public class CardResponse
    {
        public Guid CardId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? Description { get; set; }  // Nullable karena Description bisa kosong
        public Guid ListId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
