namespace TornOps.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using TornOps.Models;
using TornOps.Helpers;

public partial class StatusViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAbroad))]
    [NotifyPropertyChangedFor(nameof(InHospital))]
    [NotifyPropertyChangedFor(nameof(InJail))]
    [NotifyPropertyChangedFor(nameof(IconPath))]
    public partial string? State { get; private set; }

    [ObservableProperty] public partial string? Description { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasDetails))]
    [NotifyPropertyChangedFor(nameof(DetailsPlain))]
    public partial string? Details { get; private set; }

    // UI-friendly text
    public string? DetailsPlain => TextHelpers.CleanDetailsKeepName(Details);
    public bool HasDetails => !string.IsNullOrWhiteSpace(DetailsPlain);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasTimer))]
    [NotifyPropertyChangedFor(nameof(TimerDisplay))]
    public partial DateTimeOffset? UntilUtc { get; private set; }

    
    public bool IsAbroad => string.Equals(State, "Traveling", StringComparison.OrdinalIgnoreCase)
                          || string.Equals(State, "Abroad", StringComparison.OrdinalIgnoreCase);
    public bool InHospital => string.Equals(State, "Hospital", StringComparison.OrdinalIgnoreCase);
    public bool InJail => string.Equals(State, "Jail", StringComparison.OrdinalIgnoreCase);

    // Show a countdown only when UntilUtc is in the future
    public bool HasTimer => UntilUtc.HasValue && UntilUtc.Value > DateTimeOffset.UtcNow;
    public string TimerDisplay =>
        HasTimer
            ? StatFormatter.FormatDdHhMmSs((int)Math.Max(0, (UntilUtc!.Value - DateTimeOffset.UtcNow).TotalSeconds))
            : string.Empty;


    public string? IconPath => Icons.StatusFor(State);


    public void UpdateFrom(StatusModel? s)
    {
        State = s?.State;
        Description = s?.Description;
        Details = s?.Details?.Trim();
        UntilUtc = (s?.Until is long u && u > 0) ? DateTimeOffset.FromUnixTimeSeconds(u) : null;
    }
}
