using BL.Models;
using Dal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBLSubCategory: ICrud<BLSubCategory>
    {
        
        List<BLSubCategory> GetAllByCategory(int idCategory);
       
    }
}
