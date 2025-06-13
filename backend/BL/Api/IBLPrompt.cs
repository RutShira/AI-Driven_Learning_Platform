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
        Task<string> ProcessPromptAsync(BLPrompt prompt);
        Task CreateAsync(BLPrompt entity); // Removed 'async' modifier as interfaces cannot have method bodies.  
        Task<string> CallOpenAiApi(string promptText);
        Task<BLPrompt> GetPromptByIdAsync(int id);
        Task UpdateAsync(BLPrompt entity);
        Task DeleteAsync(int id);
        Task<List<BLPrompt>> GetAllAsync();
        Task<List<BLPrompt>> GetPromptsByUserIdAndSubCategoryAsync(int userId, int subCategoryId);

    }
}
