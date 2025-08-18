namespace TornOps.Utils
{
    public static class StatFormatter
    {
        public static string FormatFullTime(int fulltimeSeconds)
        {
            if (fulltimeSeconds <= 0) return "FULL";

            TimeSpan ts = TimeSpan.FromSeconds(fulltimeSeconds);
            return ts.ToString(@"hh\h\ mm\m");
        }

        public static string FormatStat(int current, int maximum)
        {
            return $"{current} / {maximum}";
        }
        public static string MoneyOrDash(long? v) => v.HasValue ? $"${v.Value:N0}" : "—";
        public static string CountOrDash(int? v) => v.HasValue ? v.Value.ToString("N0") : "—";

        public static string TimeOrDash(int? seconds)
            => seconds.HasValue
                ? (seconds.Value > 0 ? StatFormatter.FormatFullTime(seconds.Value) : "FULL")
                : "—";

    }
}
