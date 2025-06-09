using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
