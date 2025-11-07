namespace TornOps.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using TornOps.Models;
using TornOps.Helpers;


/// <summary>ViewModel specifically for managing chains.</summary>
public partial class ChainViewModel : ObservableObject
{
    private static readonly int[] Milestones =
    {
        10, 25, 50, 100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000
    };

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NextMilestone))]
    [NotifyPropertyChangedFor(nameof(PrevMilestone))]
    [NotifyPropertyChangedFor(nameof(MilestonePercent))]
    [NotifyPropertyChangedFor(nameof(ProgressFormatted))]
    public partial int Current { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MilestonePercent))]
    [NotifyPropertyChangedFor(nameof(ProgressFormatted))]
    public partial int Maximum { get; private set; }

    [ObservableProperty]
    public partial int Modifier { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TimerDisplay))]
    public partial int Cooldown { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TimerDisplay))] 
    public partial int Timeout { get; private set; }

    public int NextMilestone => Milestones.FirstOrDefault(m => m > Current);
    public int PrevMilestone => Milestones.LastOrDefault(m => m <= Current);

    public double MilestonePercent =>
        NextMilestone > PrevMilestone
            ? (double)(Current - PrevMilestone) / (NextMilestone - PrevMilestone)
            : 1.0;

    /// <summary>Displays chain progress eg "123 / 250".</summary>
    public string ProgressFormatted =>
        NextMilestone > 0 ? $"{Current} / {NextMilestone}" : $"{Current}";

    public string CooldownFormatted => StatFormatter.FormatCooldownOrNone(Cooldown);

    /// <summary>Displays remaining time for both cooldown and timeout.
    /// Currently does not state which is being referenced.</summary>
    public string TimerDisplay =>
    Cooldown > 0
        ? StatFormatter.FormatDdHhMm(Cooldown)
        : (Current > 0
            ? (Timeout > 0 ? StatFormatter.FormatDdHhMmSs(Timeout) : "Expired")
            : "Ready");

    public void UpdateFrom(ChainModel? c)
    {
        if (c is null) 
        {
            Current = 0; 
            Maximum = 0; 
            Modifier = 0; 
            Cooldown = 0;
            Timeout = 0;
            return; 
        }
        Current = c.Current;
        Maximum = c.Maximum;
        Modifier = c.Modifier;
        Cooldown = c.Cooldown;
        Timeout = c.Timeout;
    }
}
