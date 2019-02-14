using Recipes.DAL.Entities;
using System.Collections.Generic;

namespace Recipes.DAL.Schema
{
    public interface IIngredientsReadRepository
    {
        Ingredient GetIngredientByName(string name);
        IEnumerable<Ingredient> GetAllIngredients();
    }
}
