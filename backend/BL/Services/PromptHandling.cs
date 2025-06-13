using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BL.Exception;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace BL.Services
{
    public class PromptHandling : IBLPrompt
    {

        private readonly IPrompt _learningRepository;
        private readonly ISubCategory _subCategoryRepository;
        private readonly ICategory _categoryRepository;
         private readonly OpenAiSettings _openAiSettings;

      
        public PromptHandling(IOptions<OpenAiSettings> openAiSettings, IDal learningRepository)
        {
            _learningRepository = learningRepository.Prompt;
            _subCategoryRepository = learningRepository.SubCategory;
            _categoryRepository = learningRepository.Category;
            _openAiSettings = openAiSettings.Value;
        }
        public async Task<string> ProcessPromptAsync(BLPrompt prompt)
        {
            ValidatePrompt(prompt);
            try
            {
                // המרת BLPrompt ל-Prompt
                var dalPrompt = new Prompt
                {

                    UserId = prompt.UserId,
                    CategoryId = prompt.CategoryId,
                    SubCategoryId = prompt.SubCategoryId,
                    Prompt1 = prompt.Prompt1,
                    CreatedAt = DateTime.UtcNow // או כל תאריך אחר שתרצה
                };
               
                    // שליחת הפנייה ל-AI
                    var aiResponse = await CallOpenAiApi(prompt.Prompt1);
                if (true)
                {
                    
                }
                bool b= await ValidateRequest( new ValidatePromptRequest
                {
                    UserPrompt = prompt.Prompt1,
                    CategoryName = _categoryRepository.Read(prompt.CategoryId)?.Name ?? "Unknown",
                    SubCategoryName = _subCategoryRepository.Read(prompt.SubCategoryId)?.Name ?? "Unknown"
                });
                if (b)
                {

                    // שמירת התגובה בבסיס הנתונים
                    prompt.Response = aiResponse;
                    await CreateAsync(prompt);

                    return aiResponse;
                }
                else
                {
                    throw new HttpRequestException("הפנייה אינה מתאימה לקטגוריה או תת־קטגוריה שנבחרו.");
                }
                } // Fix missing return statement
            catch (DbUpdateException ex)
            {
                // טיפול בשגיאות של עדכון בבסיס הנתונים
                throw new ServiceException("שגיאה בהוספת הפנייה לבסיס הנתונים.", ex);
            }
            catch (HttpRequestException ex)
            {
                // הצגת השגיאה למשתמש
            string errorMessage = $"שגיאה: {ex.Message}"; // כאן נשלוף את הודעת השגיאה
                Console.WriteLine(errorMessage); // או להציג למשתמש בדרך אחרת
                // לזרוק את השגיאה הלאה
                throw new HttpRequestException("הפנייה אינה מתאימה לקטגוריה או תת־קטגוריה שנבחרו.", ex);
            
          
            }
            catch (System.Exception ex)
            {
                // טיפול בשגיאות כלליות
                throw new ServiceException("שגיאה בלתי צפויה.", ex);
            }
        }

     

        private async Task<string> CallOpenAiApi(string promptText)
        {
            using (var httpClient = new HttpClient())
            {
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                new { role = "user", content = promptText }
            },
                    max_tokens = 100
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAiSettings.ApiKey);

                var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OpenAiResponse>(responseBody);

                return result.choices[0].message.content; // החזרת התגובה מה-AI
            }
        }


        public async Task<List<BLPrompt>> GetUserPromptsAsync(int userId)
        {
            try
            {
                return (List<BLPrompt>)await GetPromptsByUserIdAsync(userId);
            }
            catch (System.Exception ex)
            {
                throw new ServiceException("שגיאה בהבאת ההיסטוריה של המשתמש.", ex);
            }
        }

        private void ValidatePrompt(BLPrompt prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt.Prompt1))
            {
                throw new ValidationException("הטקסט של הפנייה לא יכול להיות ריק.");
            }
           
            if (prompt.UserId <= 0)
            {
                throw new ValidationException("משתמש חייב להיות תקף.");
            }
            Category category = _categoryRepository.Read(prompt.CategoryId);
            if (category == null)
            {
                throw new ValidationException("הקטגוריה אינה קיימת.");
            }
            SubCategory? bLSubCategory = _subCategoryRepository.GetAllByCategory(prompt.CategoryId).Find(e => e.SubCategoryId == prompt.SubCategoryId); // קריאה לפונקציה Read כדי לוודא שהפנייה קיימת
            if (bLSubCategory == null)
            {
                throw new ValidationException("תת הקטגוריה אינה קיימת.");
            }
            // בדיקת תוכן: לא לאפשר טקסטים פוגעניים או ריקים מתוכן
            var forbiddenWords = new[] { "קללה", "פוגעני", "offensive" };
            foreach (var word in forbiddenWords)
            {
                if (prompt.Prompt1.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ValidationException("הפנייה מכילה תוכן לא הולם.");
                }
            }

            // בדיקת הלימות: הפנייה צריכה להכיל לפחות 5 תווים שאינם רווח
            if (prompt.Prompt1.Trim().Length < 2)
            {
                throw new ValidationException("הפנייה קצרה מדי.");
            }
        }

        public async Task<IEnumerable<BLPrompt>> GetPromptsByUserIdAsync(int userId)
        {
            return await Task.Run(() =>
                _learningRepository.GetAll()
                    .Where(p => p.UserId == userId)
                    .Select(p => new BLPrompt
                    {
                        Id=p.Id,
                        UserId = p.UserId,
                        CategoryId = p.CategoryId,
                        SubCategoryId = p.SubCategoryId,
                        Prompt1 = p.Prompt1,
                        Response = p.Response,
                        CreatedAt = p.CreatedAt
                    })
                    .ToList()
            );
        }

        async Task CreateAsync(BLPrompt entity)
        {
            // Implementation for asynchronous creation of a Prompt entity  
            await Task.Run(() => { Prompt p = _learningRepository.Create(new Prompt
            {
                UserId = entity.UserId,
                CategoryId = entity.CategoryId,
                SubCategoryId = entity.SubCategoryId,
                Prompt1 = entity.Prompt1,
                Response = entity.Response,
                CreatedAt = DateTime.UtcNow // or any other date you want  
            }
            );}
            );
        }


        public BLPrompt Read(int id)
        {
            try
            {
                var prompt = _learningRepository.Read(id);
                if (prompt == null)
                {
                    throw new NotFoundException($"Prompt with ID {id} not found.");
                }

                return new BLPrompt
                {
                    Id = prompt.Id,
                    UserId = prompt.UserId,
                    CategoryId = prompt.CategoryId,
                    SubCategoryId = prompt.SubCategoryId,
                    Prompt1 = prompt.Prompt1,
                    Response = prompt.Response,
                    CreatedAt = prompt.CreatedAt
                };
            }

            catch (System.Exception ex)
            {
                throw new ServiceException("Error retrieving the prompt.", ex);
            }
        }



        public void Update(BLPrompt entity)
        {
            try
            {
                var existingPrompt = _learningRepository.Read(entity.Id);
                if (existingPrompt == null)
                {
                    throw new NotFoundException($"Prompt with ID {entity.Id} not found.");
                }

                // Validate the prompt before updating
                ValidatePrompt(entity);

                existingPrompt.Prompt1 = entity.Prompt1;
                existingPrompt.Response = entity.Response;
                existingPrompt.CategoryId = entity.CategoryId;
                existingPrompt.SubCategoryId = entity.SubCategoryId;

                _learningRepository.Update(existingPrompt);
            }
            catch (System.Exception ex)
            {
                throw new ServiceException("Error updating the prompt.", ex);
            }
        }


        public void Delete(int id)
        {
            try
            {
                var prompt = _learningRepository.Read(id);
                if (prompt == null)
                {
                    throw new NotFoundException($"Prompt with ID {id} not found.");
                }

                _learningRepository.Delete(id);
            }
            catch (System.Exception ex)
            {
                throw new ServiceException("Error deleting the prompt.", ex);
            }
        }






        Task<string> IBLPrompt.CallOpenAiApi(string promptText)
        {
            return CallOpenAiApi(promptText);
        }

        Task IBLPrompt.CreateAsync(BLPrompt entity)
        {
            return CreateAsync(entity);
        }

        public async Task<IEnumerable<BLPrompt>> GetPromptsByUserIdAndCategotyAsync(int userId, int idCat)
        {
            return await Task.Run(() =>
    _learningRepository.GetAll()
        .Where(p => p.UserId == userId&& p.CategoryId==idCat)
        .Select(p => new BLPrompt
        {Id = p.Id,
            UserId = p.UserId,
            CategoryId = p.CategoryId,
            SubCategoryId = p.SubCategoryId,
            Prompt1 = p.Prompt1,
            Response = p.Response,
            CreatedAt = p.CreatedAt
        })
        .ToList()
);
        }

        private async Task<bool> ValidateRequest(ValidatePromptRequest request)
        {
            string prompt = $@"
                   הטקסט הבא הוא שאלה או בקשה של משתמש:
                   ""{request.UserPrompt}""
                   האם השאלה קשורה לנושא '{request.CategoryName}' או תת־הנושא '{request.SubCategoryName}'?
                   אם כן, ענה 'כן'. אם לא, ענה 'לא'.";

            
            string aiResponse = await CallOpenAiApi(prompt);
           
            string answer = aiResponse.Trim().ToLower();
            if (!(answer.StartsWith("כן") || answer.StartsWith("yes")))
            {
                return false;
            }
            return true;
        }

        public Task<BLPrompt> GetPromptByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                var prompt = _learningRepository.Read(id);
                if (prompt == null)
                {
                    throw new NotFoundException($"Prompt with ID {id} not found.");
                }
                return new BLPrompt
                {
                    Id = prompt.Id,
                    UserId = prompt.UserId,
                    CategoryId = prompt.CategoryId,
                    SubCategoryId = prompt.SubCategoryId,
                    Prompt1 = prompt.Prompt1,
                    Response = prompt.Response,
                    CreatedAt = prompt.CreatedAt
                };
            });

        }

        public Task UpdateAsync(BLPrompt entity)
        {
            return Task.Run(() =>
            {
                var existingPrompt = _learningRepository.Read(entity.Id);
                if (existingPrompt == null)
                {
                    throw new NotFoundException($"Prompt with ID {entity.Id} not found.");
                }
                // Validate the prompt before updating
                ValidatePrompt(entity);
                existingPrompt.Prompt1 = entity.Prompt1;
                existingPrompt.Response = entity.Response;
                existingPrompt.CategoryId = entity.CategoryId;
                existingPrompt.SubCategoryId = entity.SubCategoryId;
                _learningRepository.Update(existingPrompt);
            });

        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() =>
            {
                var prompt = _learningRepository.Read(id);
                if (prompt == null)
                {
                    throw new NotFoundException($"Prompt with ID {id} not found.");
                }
                _learningRepository.Delete(id);
            });

        }

        public async Task<List<BLPrompt>> GetAllAsync()
        {
            var prompts = await _learningRepository.GetAllAsync(); // הפוך גם את הריפוזיטורי לאסינכרוני אם אפשר

            return prompts.Select(p => new BLPrompt
            {
                Id = p.Id,
                UserId = p.UserId,
                CategoryId = p.CategoryId,
                SubCategoryId = p.SubCategoryId,
                Prompt1 = p.Prompt1,
                Response = p.Response,
                CreatedAt = p.CreatedAt
            }).ToList();
        }





        public Task<List<BLPrompt>> GetPromptsByUserIdAndSubCategoryAsync(int userId, int subCategoryId)
        {
            return Task.Run(() =>
                _learningRepository.GetAll()
                    .Where(p => p.UserId == userId && p.SubCategoryId == subCategoryId)
                    .Select(p => new BLPrompt
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        CategoryId = p.CategoryId,
                        SubCategoryId = p.SubCategoryId,
                        Prompt1 = p.Prompt1,
                        Response = p.Response,
                        CreatedAt = p.CreatedAt
                    })
                    .ToList()
            );
        }
    }
}

