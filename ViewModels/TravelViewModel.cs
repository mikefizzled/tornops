namespace TornOps.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using TornOps.Models;
using TornOps.Helpers;

public partial class TravelViewModel : ObservableObject
{
    // Needed for icon only
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IconPath))]
    public partial string? Destination { get; private set; }

    // Exact seconds for countdown/progress
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsTravelling))]
    [NotifyPropertyChangedFor(nameof(TimerDisplay))]
    [NotifyPropertyChangedFor(nameof(Progress))]
    public partial int TimeLeft { get; private set; }

    // Optional: compute from Unix times if present
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Progress))]
    public partial DateTimeOffset? DepartureUtc { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Progress))]
    public partial DateTimeOffset? ArrivalUtc { get; private set; }

    public bool IsTravelling => TimeLeft > 0;

    // Always exact (HH:mm:ss or Dd HH:mm:ss)
    public string TimerDisplay => StatFormatter.FormatDdHhMmSs(TimeLeft);

    public double Progress
    {
        get
        {
            if (DepartureUtc is not null && ArrivalUtc is not null)
            {
                var total = (ArrivalUtc.Value - DepartureUtc.Value).TotalSeconds;
                if (total <= 0) return 0;
                var elapsed = total - Math.Max(0, TimeLeft);
                var p = elapsed / total;
                return Math.Clamp(p, 0, 1);
            }
            return IsTravelling ? 0 : 1;
        }
    }

    // Flows from your fl_<slug>.svg naming
    public string IconPath => Icons.FlagFor(Destination);

    public void UpdateFrom(TravelModel? t)
    {
        Destination = t?.Destination;
        TimeLeft = t?.TimeLeft ?? 0;

        if (t?.Departed is long dep)
            DepartureUtc = DateTimeOffset.FromUnixTimeSeconds(dep);
        else
            DepartureUtc = null;

        if (t?.Timestamp is long arr)
            ArrivalUtc = DateTimeOffset.FromUnixTimeSeconds(arr);
        else
            ArrivalUtc = null;
    }
}
