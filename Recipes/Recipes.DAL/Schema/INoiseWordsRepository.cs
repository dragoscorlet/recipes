using System.Collections.Generic;

namespace Recipes.DAL.Schema
{
    public interface INoiseWordsRepository
    {
        IEnumerable<string> GetWords(string language);
    }
}
