using SHROH.Models;
using SHROH.Services;

namespace SHROH.Helpers
{
    public static class SettingsHelper
    {
        private static readonly Dictionary<string, double> ConversionRates = new()
        {
            { "USD", 1.0 },
            { "GBP", 0.8 },
            { "EUR", 0.9 }
        };

        public static async Task<(string currency, double rate)> GetCurrencyInfoAsync()
        {
            var settings = await DatabaseService.Instance.GetSettingsAsync();
            string currency = settings.FirstOrDefault()?.Currency ?? "USD";
            double rate = ConversionRates.ContainsKey(currency) ? ConversionRates[currency] : 1.0;
            return (currency, rate);
        }

        public static string FormatAmount(double amount, double rate, string currency)
        {
            var symbol = currency switch
            {
                "USD" => "$",
                "GBP" => "£",
                "EUR" => "€",
                _ => "$"
            };

            return $"{symbol}{(amount * rate):N2}";
        }
    }
}
