using Dal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  


namespace BL.Models
{
    public class BLCategory
    {

        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The name is required.")] // חובה עם הודעת שגיאה
        [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")] // אורך מקסימלי
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")] // אורך מקסימלי
        public string? Description { get; set; }

        public virtual ICollection<BLSubCategory>? SubCategories { get; set; } = new List<BLSubCategory>();
    }
}

