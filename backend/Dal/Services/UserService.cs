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
    public class UserService : IUser
    {
        private readonly DatabaseManager _context;
        public UserService(DatabaseManager db)
        {
            _context = db;
        }
        public User? Create(User entity)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // הפעלת IDENTITY_INSERT
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Users ON");

                    // הכנס את המשתמש
                    _context.Users.Add(entity);
                    _context.SaveChanges();

                    // כיבוי IDENTITY_INSERT
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Users OFF");

                    transaction.Commit();
                    // החזרת המשתמש שנוצר
                    return _context.Users.FirstOrDefault(e => e.Name == entity.Name);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error creating the user.", ex);
                }
            }
        }



        public void Delete(int id)
        {
            var user = Read(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }


        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }


        public User Read(int id)
        {
            return _context.Users.Find(id);
        }


        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }

    }
}
