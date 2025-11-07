using CommunityToolkit.Mvvm.ComponentModel;
using TornOps.Models;
using TornOps.Helpers;

namespace TornOps.ViewModels
{
    /// <summary>ViewModel for a single stat bar (Energy/Nerve/Happy/Life).</summary>
    public partial class StatBarViewModel : ObservableObject
    {
        // Inputs (readable from UI, only set inside this VM).
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Percent))]
        [NotifyPropertyChangedFor(nameof(Formatted))]
        [NotifyPropertyChangedFor(nameof(TimeRemaining))]
        public partial int Current { get; private set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Percent))]
        [NotifyPropertyChangedFor(nameof(Formatted))]
        [NotifyPropertyChangedFor(nameof(TimeRemaining))]
        public partial int Maximum { get; private set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TimeRemaining))]
        public partial int Fulltime { get; private set; }

        public double Percent => Maximum > 0 ? (double)Current / Maximum : 0;

        public string Formatted => StatFormatter.FormatStat(Current, Maximum);

        public string TimeRemaining => StatFormatter.FormatBarTime(Fulltime);

        public void UpdateFrom(BarSegment? bar)
        {
            if (bar is null)
            {
                Current = 0;
                Maximum = 0;
                Fulltime = 0;
                return;
            }

            Current = bar.Current;
            Maximum = bar.Maximum;
            Fulltime = bar.Fulltime;
        }

    }
}