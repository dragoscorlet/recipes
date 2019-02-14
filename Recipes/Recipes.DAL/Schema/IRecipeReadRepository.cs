using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.DAL.Schema
{
    public interface IRecipeReadRepository
    {
        IEnumerable<string> GetAllBrands();
    }
}
