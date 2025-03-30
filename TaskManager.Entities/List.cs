using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Entities;

[Table("List")]
public partial class List
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public int Order { get; set; }

    public Guid BoardId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("BoardId")]
    [InverseProperty("Lists")]
    public virtual Board Board { get; set; } = null!;

    [InverseProperty("List")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
