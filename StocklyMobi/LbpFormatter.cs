using System;
using System.Globalization;

namespace StocklyMobi
{
    /// <summary>
    /// Display helper for Lebanese Lira amounts.
    /// It rounds only the shown value to the nearest 1,000 L.L.
    /// Calculations should continue using the original decimal/double values.
    /// </summary>
    public static class LbpFormatter
    {
        public static decimal RoundToNearestThousand(decimal amount)
        {
            return Math.Round(amount / 1000m, 0, MidpointRounding.AwayFromZero) * 1000m;
        }

        public static string Format(decimal amount)
        {
            return "L.L " + RoundToNearestThousand(amount).ToString("#,##0");
        }

        public static string Format(double amount)
        {
            return Format(Convert.ToDecimal(amount));
        }

        public static string Format(int amount)
        {
            return Format(Convert.ToDecimal(amount));
        }

        public static string FormatSuffix(decimal amount)
        {
            return RoundToNearestThousand(amount).ToString("#,##0") + " L.L";
        }

        public static string FormatSuffix(double amount)
        {
            return FormatSuffix(Convert.ToDecimal(amount));
        }

        public static string FormatText(object value)
        {
            return Format(ParseAmount(value));
        }

        public static decimal ParseAmount(object value)
        {
            if (value == null) return 0m;

            string clean = Convert.ToString(value) ?? string.Empty;
            clean = clean.Replace("L.L", string.Empty)
                         .Replace("LL", string.Empty)
                         .Replace("LBP", string.Empty)
                         .Replace("$", string.Empty)
                         .Replace(",", string.Empty)
                         .Trim();

            decimal amount;
            if (decimal.TryParse(clean, NumberStyles.Any, CultureInfo.CurrentCulture, out amount))
                return amount;

            if (decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out amount))
                return amount;

            return 0m;
        }
    }
}
