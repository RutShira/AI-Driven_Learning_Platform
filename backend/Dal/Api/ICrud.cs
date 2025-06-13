using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface ICrud<T>
    {

            T Create(T entity);
            T Read(int id);
            void Update(T entity);
            void Delete(int id);
            IEnumerable<T> GetAll();
       

    }
}
