using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class ValidatePromptRequest
    {
        public string UserPrompt { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }
}
