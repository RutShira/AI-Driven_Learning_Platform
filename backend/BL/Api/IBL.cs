using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBL
    {
        
        public IBLUser User { get; }
        public IBLCategory Category { get; }
        public IBLPrompt Prompt { get; }
        public IBLSubCategory SubCategory { get; }
    }
}
