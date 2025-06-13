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

        public BLUser Create(BLUser entity)
        {
            try
            {
                User user=_user.Create(new User
                {
                    UserId = entity.UserId,
                    Name = entity.Name,
                    Phone = entity.Phone,
                    Email = entity.Email,
                    Role = entity.Role
                });
                return new BLUser
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    Role = user.Role
                };
            }
            catch (SqlException dbEx)
            {
                // טיפול בשגיאת עדכון בבסיס הנתונים
                Console.WriteLine($"Database error: {dbEx.Message}");
                throw new System.Exception($"Database error: {dbEx.Message}", dbEx);
            }
            catch (System.Exception ex)
            {
                // טיפול בשגיאות אחרות
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new System.Exception($"An error occurred: {ex.Message}", ex);

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
            catch (System.Exception ex)
            {
                // טיפול בשגיאה המתאימה (לוג, זריקת שגיאה מותאמת וכו')
                throw new System.Exception("An error occurred while deleting the category.", ex);
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
               
            });
        }

        public BLUser Read(int id)
        {
            {
                try
                {
                    User c = _user.Read(id);
                    if (c == null)
                    {
                        throw new KeyNotFoundException($"User with ID {id} not found.");
                    }
                    BLUser bLUser = new()
                    {
                        UserId = c.UserId,
                        Name = c.Name,
                        Phone = c.Phone,
                        Email = c.Email,
                        Role = c.Role,
                       

                    };
                    return bLUser;
                }
                catch (KeyNotFoundException ex)
                {
                    // טיפול בשגיאה במקרה שהישות לא נמצאה
                    Console.WriteLine(ex.Message);
                    throw new InvalidOperationException($"GetCategory failed: {ex.Message}", ex);

                }
                catch (System.Exception ex)
                {
                    // טיפול בשגיאות אחרות
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw new System.Exception($"An error occurred: {ex.Message}", ex);
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
                   

                };
                _user.Update(user);
            }
            catch (KeyNotFoundException ex)
            {
                // טיפול בשגיאה במקרה שהישות לא נמצאה
                throw new InvalidOperationException($"Update failed: {ex.Message}", ex);
            }
            catch (System.Exception ex)
            {
                // טיפול בשגיאות אחרות
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new System.Exception($"An error occurred: {ex.Message}", ex);

            }
        }
    }


}

