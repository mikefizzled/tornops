public class StatusModel
{
    public string Description { get; set; } = string.Empty;
    public string? Details { get; set; }
    public string State { get; set; } = string.Empty;
    public long? Until { get; set; } // Unix timestamp
}
