using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CategoryManagement : IBLCategory
    {
        private readonly ICategory _category;

        public CategoryManagement(IDal dal)
        {
            _category = dal.Category;

        }
        public void Create(BLCategory entity)
        {
            _category.Create(new Category
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Description = entity.Description
            });
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID must be greater than zero.", nameof(id));
            }
            if (_category.Read(id) == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            try
            {
                _category.Delete(id);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה המתאימה (לוג, זריקת שגיאה מותאמת וכו')
                throw new Exception("An error occurred while deleting the category.", ex);
            }

        }



        // הנחה שיש לך גישה ל-SubCategories דרך ה-Category
        public IEnumerable<BLCategory> GetAll()
        {
            return _category.GetAll().Select(c => new BLCategory
            {
                Name = c.Name,
                Description = c.Description,
                SubCategories = c.SubCategories?.Select(sc => new BLSubCategory
                {
                    Name = sc.Name,
                    CategoryId = sc.CategoryId,

                }).ToList() ?? new List<BLSubCategory>()
            });
        }


    
       

        public BLCategory Read(int id)
        {
            try
            {
                Category c = _category.Read(id);
                BLCategory bLCategory = new()
                {
                    Name = c.Name,
                    Description = c.Description,
                    SubCategories = c.SubCategories?.Select(sc => new BLSubCategory
                    {
                        Name = sc.Name,
                        CategoryId = sc.CategoryId,
                    }).ToList() ?? new List<BLSubCategory>()

                };
                return bLCategory;
            }
            catch (KeyNotFoundException ex)
            {
                // טיפול בשגיאה במקרה שהישות לא נמצאה
                Console.WriteLine(ex.Message);
                throw new InvalidOperationException($"GetCategory failed: {ex.Message}", ex);

            }
        }

        public void Update(BLCategory entity)
        {
            try
            {
                Category category = new()
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    SubCategories = entity.SubCategories?.Select(sc => new SubCategory
                    {
                        Name = sc.Name,
                        CategoryId = sc.CategoryId,
                    }).ToList() ?? new List<SubCategory>(),

                };
                _category.Update(category);
            }
            catch (KeyNotFoundException ex)
            {
                // טיפול בשגיאה במקרה שהישות לא נמצאה
                throw new InvalidOperationException($"Update failed: {ex.Message}", ex);
            }
        }
    }
}
