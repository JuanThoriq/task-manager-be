using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Entities;

[Table("Board")]
public partial class Board
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string OrgId { get; set; } = null!;

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [InverseProperty("Board")]
    public virtual ICollection<List> Lists { get; set; } = new List<List>();
}
