using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class CategoryService : ICategory
    {

        private readonly DatabaseManager _context;
        public CategoryService(DatabaseManager db)
        {
            _context = db;
        }
        public Category? Create(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
            return _context.Categories?.FirstOrDefault(e => e.Name == entity.Name);

        }


        public void Delete(int id)
        {
            var category = Read(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }


        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }


        public Category? Read(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
        }

    }
}
