using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class SubCategoryService : ISubCategory
    {
        private readonly DatabaseManager _context;
        public SubCategoryService(DatabaseManager db)
        {
            _context = db;
        }
        public SubCategory? Create(SubCategory entity)
        {
            _context.SubCategories.Add(entity);
            _context.SaveChanges();
            return _context.SubCategories?.FirstOrDefault(e => e.Name == entity.Name);
        }


        public void Delete(int id)
        {
            var subCategory = Read(id);
            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
                _context.SaveChanges();
            }
        }


        public IEnumerable<SubCategory> GetAll()
        {
            return _context.SubCategories.ToList();
        }

        public List<SubCategory> GetAllByCategory(int idCategory)
        {
            return _context.SubCategories
                .Where(sc => sc.CategoryId == idCategory)
                .ToList();
        }

        public SubCategory Read(int id)
        {
            return _context.SubCategories.Find(id);
        }


        public void Update(SubCategory entity)
        {
            _context.SubCategories.Update(entity);
            _context.SaveChanges();
        }

    }
}
