using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDal
    {
        public IUser User { get; }
        public ICategory Category { get; }
        public IPrompt Prompt { get; }
        public ISubCategory SubCategory { get; }


    }
}
