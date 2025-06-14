using BL.Api;

namespace BL
{
    public class BLManager : IBL
    {
        public IBLUser User { get; }
        public IBLCategory Category { get; }
        public IBLPrompt Prompt { get; }
        public IBLSubCategory SubCategory { get; }

        public BLManager(
            IBLUser user,
            IBLCategory category,
            IBLPrompt prompt,
            IBLSubCategory subCategory)
        {
            User = user;
            Category = category;
            Prompt = prompt;
            SubCategory = subCategory;
        }
    }
}