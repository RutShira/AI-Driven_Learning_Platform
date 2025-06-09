using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Prompt
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string Prompt1 { get; set; } = null!;

    public string Response { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual SubCategory SubCategory { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
