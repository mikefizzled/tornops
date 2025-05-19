public class TravelModel
{
    public string Destination { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public long Timestamp { get; set; }
    public long Departed { get; set; }
    public int TimeLeft { get; set; }
}
