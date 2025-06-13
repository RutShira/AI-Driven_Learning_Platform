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
        private readonly ICategory _categoryRepository;

        public SubCategoryManagment(IDal learningRepository)
        {
            _subCategoryRepository = learningRepository.SubCategory;
            _categoryRepository = learningRepository.Category;
        }
   
        public BLSubCategory Create(BLSubCategory entity)
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
           var v= _categoryRepository.Read(entity.CategoryId) ?? throw new KeyNotFoundException($"Category with ID {entity.CategoryId} not found.");
            SubCategory subCategory= _subCategoryRepository.Create(new SubCategory
            {
                SubCategoryId = entity.SubCategoryId,
                Name = entity.Name,
                CategoryId = entity.CategoryId,
                Category = v

            });

           return new BLSubCategory
            {
                SubCategoryId = subCategory.SubCategoryId,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId
            };


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

            List<BLSubCategory> v=_subCategoryRepository.GetAll().Select(sc => new BLSubCategory
            {
                SubCategoryId = sc.SubCategoryId,
                Name = sc.Name,
                CategoryId = sc.CategoryId
            }).ToList();

            return v;

        }

        public List<BLSubCategory> GetAllByCategory(int idCategory)
        {
            if (idCategory <= 0)
            {
                throw new ArgumentException("Category ID must be greater than zero.", nameof(idCategory));
            }

            var subCategories = _subCategoryRepository.GetAll().Where(e=>e.CategoryId== idCategory);
            
            return subCategories.Select(sc => new BLSubCategory
            {
                SubCategoryId= sc.SubCategoryId,
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
                SubCategoryId = subCategory.SubCategoryId,
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
