using Dal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BLSubCategory
    {
        [Key] // מציין שמדובר במפתח ראשי
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // מפתח רץ
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")] // חובה עם הודעת שגיאה
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")] // אורך מקסימלי
        public string Name { get; set; } = null!;

        [Required] // חובה
        public int CategoryId { get; set; }



    }
}

