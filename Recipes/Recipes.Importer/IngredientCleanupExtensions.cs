using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Recipes.Importer
{
    public static class IngredientCleanupExtensions
    {
        private static List<string> _blackList = new List<string>
        {
            "",
            "litru",
            "",
            "",
            "bucheţele",
            "creanguta",
            "crenguta",
            "legatura",
            "lingurita",
            "lingurite",
            "legatura",
            "kg",
            "zeama de la",
            "-",
            "fire",
            "medalioane de",
            " o ",
            "călduţ",
            "pentru călit",
            "frunze",
            "sau roşie",
            "un ",
            "un baton mic ",
            "pliculete",
            "miezul de la o ",
            " pentru uns",
            "bucăţi de",
            "capa\u001dâna mare de",
            "/ ta",
            "/ ",
            "/",
            "linguri",
            "zeamă de ",
            "de ",
            "lingura",
            "sauceapă roşie",
            "cana",
            "+",
            "bucati",
            "bucata",
            "căpăţână",
            "]",
            "legătură",
            "felie",
            "pungă",
            ",",
            "(subţiri)",
            "(preferabil fleică)",
            "litri",
            ".",
            "(şuncă)",
            "( cm)",
            "(din magazin)",
            "pachet",
            "zahăr tos",
            "pumn",
            "sănătoase, bine coapte",
            " mică",
            " mare",
            "%",
            "(preferat brun)",
            "(preferat măsline)",
            "(toast)",
            "oțet aromatic",
            "(opțional)",
            "taiate in sferturi",
            "ceasca",
            "medie",
            "moale",
            "borcan",
            "pentru decor",
            "la temperatura camerei",
            "pentru servit",
            "cani",
            "bucatele",
            "cesti",
            " (conserva)",
            "lingură",
            "(optional)",
            "care creste singura",
            "tăiat în felii subțiri",
            "(aproximativ  grame)",
            "(după preferințe)",
            "(după preferinţă)",
            "cubulete sau fasii",
            "cu vârf",
            "sticlă",
            " ml",
            " mici",
            "foarte rece",
            "capatani intregi",
            "calda",
            "vertocata marunt",
            "vertocata fin",
            " ver",
            "apa",
            "mâini",
            "grame",
            "(",
            "taiata în jumatate lungime și cu semintele scoase",
            "mâna",
            "mana",
            "pâna la",
            "bucati dezosate",
            "Între  și",
            "caței",
            "taiați felioare subtiritocați",
            "catei",
            "taiati felioare subtiritocati",
            "culori diferite",
            "  g",
            "tocata",
            "tocată",
            "L ",
            "la alegere",
            "Toppinguri",
            "ctana",
            "mari",
            "medi",
            "cateva",
            "spalat si oparit"
        };

        public static string GetCleanIngredientName(this string ingredientName)
        {
            if (string.IsNullOrEmpty(ingredientName))
                return string.Empty;

            return ingredientName
                .RemoveDigits()
                .RemoveObservations()
                .RemoveIncompleteObservations()
                .RemoveInvalidWords()
                .RemoveRoCharacters()
                .RemoveOptionalIngredient().Trim();
        }

        private static string RemoveDigits(this string text)
        {
            return Regex.Replace(text, @"\d+", string.Empty);
        }

        private static string RemoveOptionalIngredient(this string text)
        {
            text = Regex.Replace(text, @"sau.+", string.Empty);
            text = Regex.Replace(text, @"\*.+", string.Empty);
            text = Regex.Replace(text, @"Optional\:.+", string.Empty);
            text = Regex.Replace(text, @"\:.+", string.Empty);
            text = Regex.Replace(text, @"^ta ", string.Empty);
            text = Regex.Replace(text, @"^te ", string.Empty);
            text = Regex.Replace(text, @"^e ", string.Empty);

            return text;
        }

        private static string RemoveObservations(this string text)
        {
            return Regex.Replace(text, @"\(.+?\)", string.Empty);
        }

        private static string RemoveIncompleteObservations(this string text)
        {
            return Regex.Replace(text, @"\(.+?", string.Empty);
        }


        private static string RemoveRoCharacters(this string text)
        {
            return text.Replace("ş", "s")
                .Replace("ț", "t")
                .Replace("ă", "a")
                .Replace("â","a");
        }

        private static string RemoveInvalidWords(this string text)
        {
            _blackList.Where(w => !string.IsNullOrEmpty(w)).ToList().ForEach(word => text = text.Replace(word, string.Empty));

            return text.Trim();
        }

    }
}
