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
    public class BLPrompt
    {
        [Key] // מציין שמדובר במפתח ראשי
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // מפתח רץ
        public int Id { get; set; }

        [Required] // חובה
        public int UserId { get; set; }

        [Required] // חובה
        public int CategoryId { get; set; }

        [Required] // חובה
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Prompt text is required.")] // חובה עם הודעת שגיאה
        [StringLength(500, ErrorMessage = "Prompt cannot exceed 500 characters.")] // אורך מקסימלי
        public string Prompt1 { get; set; } = null!;

       
        public string? Response { get; set; } 

        public DateTime? CreatedAt { get; set; }

    }

}

