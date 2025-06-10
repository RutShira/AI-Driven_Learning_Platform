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
    public class SubCategoryManagment : IBLSubCategory
    {
        private readonly ISubCategory _subCategoryRepository;

        public SubCategoryManagment(IDal learningRepository)
        {
            _subCategoryRepository = learningRepository.SubCategory;
        }

        public void Create(BLSubCategory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "The subcategory entity cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                throw new ArgumentException("Subcategory name cannot be empty.", nameof(entity.Name));
            }
            if (entity.CategoryId <= 0)
            {
                throw new ArgumentException("Category ID must be greater than zero.", nameof(entity.CategoryId));
            }
            _subCategoryRepository.Create(new SubCategory
            {
                Name = entity.Name,
                CategoryId = entity.CategoryId
            });

        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID must be greater than zero.", nameof(id));
            }
            var subCategory = _subCategoryRepository.Read(id);
            if (subCategory == null)
            {
                throw new KeyNotFoundException($"SubCategory with ID {id} not found.");
            }

            _subCategoryRepository.Delete(id);
           
        }

        public IEnumerable<BLSubCategory> GetAll()
        {
            return _subCategoryRepository.GetAll().Select(sc => new BLSubCategory
            {
                
                Name = sc.Name,
                CategoryId = sc.CategoryId
            });

        }

        public List<BLSubCategory> GetAllByCategory(int idCategory)
        {
            if (idCategory <= 0)
            {
                throw new ArgumentException("Category ID must be greater than zero.", nameof(idCategory));
            }

            var subCategories = _subCategoryRepository.GetAll().Where(e=>e.CategoryId== idCategory);
            if (subCategories == null || !subCategories.Any())
            {
                throw new KeyNotFoundException($"No subcategories found for Category ID {idCategory}.");
            }
            return subCategories.Select(sc => new BLSubCategory
            {
                Name = sc.Name,
                CategoryId = sc.CategoryId
            }).ToList();
        }

        public BLSubCategory Read(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID must be greater than zero.", nameof(id));
            }
            var subCategory = _subCategoryRepository.Read(id);
            if (subCategory == null)
            {
                throw new KeyNotFoundException($"SubCategory with ID {id} not found.");
            }
            return new BLSubCategory
            {
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId
            };

        }

        public void Update(BLSubCategory entity)
        {
            SubCategory subCategory = _subCategoryRepository.Read(entity.SubCategoryId);
            if (subCategory == null)
            {
                throw new KeyNotFoundException($"SubCategory with ID {entity.SubCategoryId} not found.");
            }
            subCategory.CategoryId = entity.CategoryId;
            subCategory.Name = entity.Name;
             _subCategoryRepository.Update(subCategory);
        }
    }
}
