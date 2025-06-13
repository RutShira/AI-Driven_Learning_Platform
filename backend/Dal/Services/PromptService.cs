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
    public class PromptService : IPrompt
    {
        private readonly DatabaseManager _context;
        public PromptService(DatabaseManager db)
        {
            _context = db;
        }
        public Prompt? Create(Prompt entity)
        {
            _context.Prompts.Add(entity);
            _context.SaveChanges();
            return _context.Prompts?.FirstOrDefault(e => e.Prompt1 == entity.Prompt1);
        }
      
       


        public void Delete(int id)
        {
            var prompt = Read(id);
            if (prompt != null)
            {
                _context.Prompts.Remove(prompt);
                _context.SaveChanges();
            }
        }


        public IEnumerable<Prompt> GetAll()
        {
            return _context.Prompts.ToList();
        }
        public async Task<List<Prompt>> GetAllAsync()
        {
            return await _context.Prompts.ToListAsync();
        }



        public Prompt? Read(int id)
        {
            return _context.Prompts.Find(id);
        }


        public void Update(Prompt entity)
        {
            _context.Prompts.Update(entity);
            _context.SaveChanges();
        }

    }
}
