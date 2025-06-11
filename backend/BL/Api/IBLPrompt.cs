using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBLPrompt
    {
        Task<IEnumerable<BLPrompt>> GetPromptsByUserIdAsync(int userId);
        Task<IEnumerable<BLPrompt>> GetPromptsByUserIdAndCategotyAsync(int userId,int idCat);
        Task<BLPrompt> ProcessPromptAsync(BLPrompt prompt);
        Task CreateAsync(BLPrompt entity); // Removed 'async' modifier as interfaces cannot have method bodies.  
        Task<string> CallOpenAiApi(string promptText);
    }
}
