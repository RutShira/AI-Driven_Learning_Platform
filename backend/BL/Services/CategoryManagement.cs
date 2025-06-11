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
        public BLCategory Create(BLCategory entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Category cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                throw new ArgumentException("Category name cannot be empty.", nameof(entity.Name));
            }
           
            Category category=   new Category
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Description = entity.Description
            };
            Category v = _category.Create(category);
            BLCategory bLCategory = new()
            {
                CategoryId = v.CategoryId,
                Name = v.Name,
                Description = v.Description,
              
            };

            return bLCategory;
          
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
            catch (System.Exception ex)
            {
                // טיפול בשגיאה המתאימה (לוג, זריקת שגיאה מותאמת וכו')
                throw new System.Exception("An error occurred while deleting the category.", ex);
            }

        }



        // הנחה שיש לך גישה ל-SubCategories דרך ה-Category
        public IEnumerable<BLCategory> GetAll()
        {
            return _category.GetAll().Select(c => new BLCategory
            {
                CategoryId=c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                SubCategories = c.SubCategories?.Select(sc => new BLSubCategory
                {
                    SubCategoryId= sc.SubCategoryId,
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
                    CategoryId=c.CategoryId,
                    Name = c.Name,
                    Description = c.Description,
                    SubCategories = c.SubCategories?.Select(sc => new BLSubCategory
                    {
                        SubCategoryId=sc.SubCategoryId,
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
                    CategoryId=entity.CategoryId,
                    Name = entity.Name,
                    Description = entity.Description,
                    SubCategories = entity.SubCategories?.Select(sc => new SubCategory
                    {
                        SubCategoryId = sc.SubCategoryId,
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
