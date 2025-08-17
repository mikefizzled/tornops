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
    }
}
