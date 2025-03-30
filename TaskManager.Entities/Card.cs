using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Entities;

[Table("Card")]
public partial class Card
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public int Order { get; set; }

    public string? Description { get; set; }

    public Guid ListId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("ListId")]
    [InverseProperty("Cards")]
    public virtual List List { get; set; } = null!;
}
