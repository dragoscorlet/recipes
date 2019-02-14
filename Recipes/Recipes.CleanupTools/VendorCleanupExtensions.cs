using System.Text.RegularExpressions;

namespace Recipes.CleanupTools
{
    public static class VendorCleanupExtensions
    {
        private const string VOLUME_PATTERN = @"(?<capture>\d+)ml";
        private const string QUANTITY_GRAMS_PATTERN = @"(?<capture>\d+)g";
        private const string QUANTITY_KG_PATTERN = @"(?<capture>\d+)kg";

        public static int GetVolumeInMl(this string vendorProductTitle)
        { 
            return GetNumericMatch(vendorProductTitle, VOLUME_PATTERN);
        }

        public static int GetQuantityInGrams(this string vendorProductTitle)
        {
            var qty = GetQuantityInGramsInternal(vendorProductTitle);

            if (qty == 0)
                return GetQuantityKgToGrams(vendorProductTitle);

            return qty;
        }

        private static int GetQuantityKgToGrams(string vendorProductTitle)
        {
           return 1000 * GetNumericMatch(vendorProductTitle, QUANTITY_KG_PATTERN);
        }

        private static int GetQuantityInGramsInternal(string vendorProductTitle)
        {
            return GetNumericMatch(vendorProductTitle, QUANTITY_GRAMS_PATTERN);
        }
        private static int GetNumericMatch(string text, string pattern)
        {
            var loweredText = text.ToLowerInvariant();

            var match = Regex.Match(loweredText, pattern);

            if (match == null || match.Groups == null || string.IsNullOrEmpty(match.Groups["capture"].Value))
                return 0;

            return int.Parse(match.Groups["capture"].Value);
        }

    }
}
