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

    [StringLength(100)]
    public string ImageId { get; set; } = null!;

    public string ImageThumbUrl { get; set; } = null!;

    public string ImageFullUrl { get; set; } = null!;

    public string ImageUserName { get; set; } = null!;

    [Column("ImageLinkHTML")]
    public string ImageLinkHtml { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [InverseProperty("Board")]
    public virtual ICollection<List> Lists { get; set; } = new List<List>();
}
