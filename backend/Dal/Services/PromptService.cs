using Dal.Api;
using Dal.Models;
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
        public void Create(Prompt entity)
        {
            _context.Prompts.Add(entity);
            _context.SaveChanges();
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
