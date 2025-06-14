using Dal.Api;

namespace Dal
{
    public class DalManager : IDal
    {
        public IUser User { get; }
        public ICategory Category { get; }
        public IPrompt Prompt { get; }
        public ISubCategory SubCategory { get; }

        public DalManager(
            IUser user,
            ICategory category,
            IPrompt prompt,
            ISubCategory subCategory)
        {
            User = user;
            Category = category;
            Prompt = prompt;
            SubCategory = subCategory;
        }
    }
}