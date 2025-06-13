using System;
using Dal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BLUser
    {
        [Required]
        [Key] // מציין שמדובר במפתח ראשי
       
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")] // חובה עם הודעת שגיאה
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")] // אורך מקסימלי
        public string Name { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid phone number format.")] // בדיקת פורמט טלפון
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")] // חובה עם הודעת שגיאה
        [EmailAddress(ErrorMessage = "Invalid email address format.")] // בדיקת פורמט אימייל
        public string Email { get; set; } = null!;

        public string? Role { get; set; }

      
    }
}
