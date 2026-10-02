using System;

namespace StocklyMobi
{
    internal static class ExchangeRateConfiguration
    {
        public const decimal DefaultRate = 89500m;

        public static decimal CurrentRate
        {
            get
            {
                try
                {
                    decimal rate = Convert.ToDecimal(
                        Properties.Settings.Default.ExchangeRateLBP);
                    return rate > 0m ? rate : DefaultRate;
                }
                catch
                {
                    return DefaultRate;
                }
            }
        }

        public static void Save(decimal rate)
        {
            if (rate <= 0m)
                throw new ArgumentOutOfRangeException(
                    "rate",
                    "Exchange rate must be greater than zero.");

            Properties.Settings.Default.ExchangeRateLBP =
                Convert.ToDouble(rate);
            Properties.Settings.Default.Save();
        }

        public static decimal UsdToLbp(decimal usd)
        {
            return LbpFormatter.RoundToNearestThousand(
                usd * CurrentRate);
        }

        public static decimal LbpToUsd(decimal lbp)
        {
            decimal rate = CurrentRate;
            if (rate <= 0m) rate = DefaultRate;

            return decimal.Round(
                lbp / rate,
                2,
                MidpointRounding.AwayFromZero);
        }

        public static string FormatRate(decimal rate)
        {
            return rate.ToString("#,##0");
        }

        public static string FormatUsdAdmin(decimal amount)
        {
            return amount.ToString("N2");
        }
    }
}
