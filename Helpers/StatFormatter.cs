namespace TornOps.Utils
{
    public static class StatFormatter
    {
        public static string FormatDdHhMm(int seconds)
        {
            if (seconds <= 0) return "FULL";
            var ts = TimeSpan.FromSeconds(seconds);
            return ts.Days > 0
                ? $"{ts.Days}d {ts.Hours}h {ts.Minutes}m"
                : $"{ts.Hours}h {ts.Minutes}m";
        }

        public static string FormatStat(int? current, int? maximum) 
            => current.HasValue && maximum.HasValue ? $"{current} / {maximum}" : "- / -";
        public static string MoneyOrUnknown(long? v) =>
            v.HasValue? $"${v.Value:N0}" : "Unknown";
        public static string CountOrUnknown(int? v) 
            => v.HasValue ? v.Value.ToString("N0") : "Unknown";

        public static string FormatBarTime(int? seconds) => seconds switch
        {
            null => "Unknown",
            <= 0 => "FULL",
            _ => FormatDdHhMm(seconds.Value)
        };
        public static string FormatCooldownOrNone(int? seconds) => seconds switch
        {
            null => "Unknown",
            <= 0 => "None",
            _ => FormatDdHhMm(seconds.Value)
        };
    }
}
