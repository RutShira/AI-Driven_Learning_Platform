using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserManagement : IBLUser
    {
        private readonly IUser _user;

        public UserManagement(IDal dal)
        {
            _user = dal.User;

        }

        public void Create(BLUser entity)
        {
            try
            {
                _user.Create(new User
                {
                    UserId = entity.UserId,
                    Name = entity.Name,
                    Phone = entity.Phone,
                    Email = entity.Email,
                    Role = entity.Role
                });
            }
            catch (SqlException dbEx)
            {
                // טיפול בשגיאת עדכון בבסיס הנתונים
                Console.WriteLine($"Database error: {dbEx.Message}");
                throw new Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות אחרות
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new Exception($"An error occurred: {ex.Message}", ex);

            }
        }





        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID must be greater than zero.", nameof(id));
            }
            if (_user.Read(id) == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            try
            {
                _user.Delete(id);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה המתאימה (לוג, זריקת שגיאה מותאמת וכו')
                throw new Exception("An error occurred while deleting the category.", ex);
            }

        }

        public IEnumerable<BLUser> GetAll()
        {
            return _user.GetAll().Select(c => new BLUser
            {
                UserId = c.UserId,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                Role = c.Role,
                Prompts = c.Prompts.Select(p => new BLPrompt
                {
                    UserId = p.UserId,
                    CategoryId = p.CategoryId,
                    SubCategoryId = p.SubCategoryId,
                    Response = p.Response,
                    Prompt1 = p.Prompt1,
                    CreatedAt = p.CreatedAt,

                }).ToList()
            });
        }

        public BLUser Read(int id)
        {
            {
                try
                {
                    User c = _user.Read(id);
                    BLUser bLUser = new()
                    {
                        UserId = c.UserId,
                        Name = c.Name,
                        Phone = c.Phone,
                        Email = c.Email,
                        Role = c.Role,
                        Prompts = c.Prompts.Select(
                            p => new BLPrompt
                            {
                                UserId = p.UserId,
                                CategoryId = p.CategoryId,
                                SubCategoryId = p.SubCategoryId,
                                Response = p.Response,
                                Prompt1 = p.Prompt1,
                                CreatedAt = p.CreatedAt,
                            }).ToList()

                    };
                    return bLUser;
                }
                catch (KeyNotFoundException ex)
                {
                    // טיפול בשגיאה במקרה שהישות לא נמצאה
                    Console.WriteLine(ex.Message);
                    throw new InvalidOperationException($"GetCategory failed: {ex.Message}", ex);

                }
                catch (Exception ex)
                {
                    // טיפול בשגיאות אחרות
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw new Exception($"An error occurred: {ex.Message}", ex);
                }
            }
        }
        public void Update(BLUser entity)
        {

            try
            {
                User user = new()
                {
                    UserId = entity.UserId,
                    Name = entity.Name,
                    Phone = entity.Phone,
                    Email = entity.Email,
                    Role = entity.Role,
                    Prompts = entity.Prompts.Select(p => new Prompt
                    {
                        UserId = p.UserId,
                        CategoryId = p.CategoryId,
                        SubCategoryId = p.SubCategoryId,
                        Response = p.Response,
                        Prompt1 = p.Prompt1,
                        CreatedAt = p.CreatedAt,
                    }).ToList()

                };
                _user.Update(user);
            }
            catch (KeyNotFoundException ex)
            {
                // טיפול בשגיאה במקרה שהישות לא נמצאה
                throw new InvalidOperationException($"Update failed: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות אחרות
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new Exception($"An error occurred: {ex.Message}", ex);

            }
        }
    }


}

