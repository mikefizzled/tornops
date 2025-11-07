namespace TornOps.Helpers
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
        /// <summary>Formats the current and max stats of a given stat bar </summary>
        /// <returns>"X/Y" or "-/-" if null</returns>
        public static string FormatStat(int? current, int? maximum) 
            => current.HasValue && maximum.HasValue ? $"{current} / {maximum}" : "- / -";
        public static string MoneyOrUnknown(long? v) =>
            v.HasValue? $"${v.Value:N0}" : "Unknown";

        /// <summary>Converts seconds until a stat is full into formatted string.</summary>
        /// <returns>"FULL", "Dd Hh Mm" or "Unknown" if null</returns>
        public static string FormatBarTime(int? seconds) => seconds switch
        {
            null => "Unknown",
            <= 0 => "FULL",
            _ => FormatDdHhMm(seconds.Value)
        };

        /// <summary>Converts seconds until empty into formatted string. e.g. Cooldown timers</summary>
        /// <returns>"None", "Dd Hh Mm" or "Unknown". if null</returns>
        public static string FormatCooldownOrNone(int? seconds)
        {
            if (!(seconds > 0)) return "None";

            var s = seconds.Value;
            var d = s / 86400; s %= 86400;
            var h = s / 3600; s %= 3600;
            var m = s / 60;

            if (d > 0) return $"{d}d {h}h";
            if (h > 0) return $"{h}h {m}m";
            return $"{m}m";
        }
        /// <summary>
        /// Exact timer for use with the Chain and Travel features.
        /// <returns>Returns "DdHhMm" if days > 0 otherwise returns "HhMmSs"</returns>
        /// </summary>
        public static string FormatDdHhMmSs(int seconds)
        {
            if (seconds < 0) seconds = 0;
            var ts = TimeSpan.FromSeconds(seconds);
            return ts.Days > 0
                ? $"{ts.Days}d {ts.Hours:D2}:{ts.Minutes:D2}"
                : $"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";
        }
    }
}
