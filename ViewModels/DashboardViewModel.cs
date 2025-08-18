#pragma warning disable MVVMTK0045

using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using TornOps.Models;
using TornOps.Utils;

namespace TornOps
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly TornApiService _service = new();

        #region Player Info
        [ObservableProperty] private string playerFormatted;
        [ObservableProperty] private int playerId;
        [ObservableProperty] private string level;
        [ObservableProperty] private int points;
        [ObservableProperty] private string moneyOnhandFormatted = "$0";
        [ObservableProperty] private string dailyNetworthFormatted = "$0";
        [ObservableProperty] private string caymanBankFormatted = "$0";
        [ObservableProperty] private string cityBankAmountFormatted = "$0";
        [ObservableProperty] private string cityBankTimeRemaining = "—";
        #endregion

        #region Status
        [ObservableProperty] private string statusDescription = "";
        [ObservableProperty] private string statusState = "";
        [ObservableProperty] private string statusColor = "";
        [ObservableProperty] private string statusUntilFormatted = "";
        #endregion

        #region Travel
        [ObservableProperty] private string travelDestination;
        [ObservableProperty] private string travelMethod;
        [ObservableProperty] private int travelTimeLeft;
        [ObservableProperty] private string travelTimeRemaining = "";
        [ObservableProperty] private string travelStatusMessage;
        #endregion

        #region Energy
        [ObservableProperty] private int energyCurrent;
        [ObservableProperty] private int energyMaximum;
        [ObservableProperty] private int energyFulltime;
        [ObservableProperty] private string energyTimeRemaining;
        [ObservableProperty] private string energyFormatted;
        public double EnergyPercent => EnergyMaximum > 0 ? (double)EnergyCurrent / EnergyMaximum : 0;
        #endregion

        #region Nerve
        [ObservableProperty] private int nerveCurrent;
        [ObservableProperty] private int nerveMaximum;
        [ObservableProperty] private int nerveFulltime;
        [ObservableProperty] private string nerveTimeRemaining;
        [ObservableProperty] private string nerveFormatted;
        public double NervePercent => NerveMaximum > 0 ? (double)NerveCurrent / NerveMaximum : 0;
        #endregion

        #region Happy
        [ObservableProperty] private int happyCurrent;
        [ObservableProperty] private int happyMaximum;
        [ObservableProperty] private int happyFulltime;
        [ObservableProperty] private string happyTimeRemaining;
        [ObservableProperty] private string happyFormatted;
        public double HappyPercent => HappyMaximum > 0 ? (double)HappyCurrent / HappyMaximum : 0;
        #endregion

        #region Life
        [ObservableProperty] private int lifeCurrent;
        [ObservableProperty] private int lifeMaximum;
        [ObservableProperty] private int lifeFulltime;
        [ObservableProperty] private string lifeTimeRemaining;
        [ObservableProperty] private string lifeFormatted;
        public double LifePercent => LifeMaximum > 0 ? (double)LifeCurrent / LifeMaximum : 0;
        #endregion

        #region Chain

        [ObservableProperty] private int chainCurrent;
        [ObservableProperty] private int chainMaximum;
        [ObservableProperty] private string chainTimeout;
        [ObservableProperty] private int chainModifier;
        [ObservableProperty] private int chainCooldown;

        [ObservableProperty] private string chainFormatted;
        [ObservableProperty] private string chainTimeRemaining;

        [ObservableProperty] private double chainPercent;

        private static readonly int[] ChainMilestones =
        {
    10, 25, 50, 100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000
};

        public int ChainNextMilestone =>
            ChainMilestones.FirstOrDefault(m => m > ChainCurrent);

        public int ChainPreviousMilestone =>
            ChainMilestones.LastOrDefault(m => m <= ChainCurrent);



        #endregion

        #region Education
        [ObservableProperty] private int educationId;
        [ObservableProperty] private long educationTimeLeft;
        [ObservableProperty] private string educationName;
        [ObservableProperty] private string educationTimeRemaining;
        #endregion

        #region Cooldowns
        [ObservableProperty] private int drugCooldown;
        [ObservableProperty] private int medicalCooldown;
        [ObservableProperty] private int boosterCooldown;
        [ObservableProperty] private string drugCooldownFormatted;
        [ObservableProperty] private string medicalCooldownFormatted;
        [ObservableProperty] private string boosterCooldownFormatted;
        #endregion

        public DashboardViewModel()
        {
            LoadAsync();
        }

        private async void LoadAsync()
        {
            
            var userData = await _service.LoadFromMauiAssetAsync("user.json");

            if (userData != null)
            {
                PlayerFormatted = $"{userData.Name} [{userData.PlayerId}]";
                Level = $"Level: {userData.Level}";
                PlayerId = userData.PlayerId ?? 0;

                if (userData.Status is not null)
                {
                    StatusDescription = userData.Status.Description;
                    StatusState = userData.Status.State;
                    StatusColor = userData.Status.Color;
                }

                UpdateBarData(
                    userData.Energy,
                    val => EnergyCurrent = val,
                    val => EnergyMaximum = val,
                    val => EnergyFulltime = val,
                    val => EnergyTimeRemaining = val,
                    val => EnergyFormatted = val
                );

                UpdateBarData(
                    userData.Nerve,
                    val => NerveCurrent = val,
                    val => NerveMaximum = val,
                    val => NerveFulltime = val,
                    val => NerveTimeRemaining = val,
                    val => NerveFormatted = val
                );

                UpdateBarData(
                    userData.Happy,
                    val => HappyCurrent = val,
                    val => HappyMaximum = val,
                    val => HappyFulltime = val,
                    val => HappyTimeRemaining = val,
                    val => HappyFormatted = val
                );

                UpdateBarData(
                    userData.Life,
                    val => LifeCurrent = val,
                    val => LifeMaximum = val,
                    val => LifeFulltime = val,
                    val => LifeTimeRemaining = val,
                    val => LifeFormatted = val
                );

                if (userData.Chain is not null)
                {
                    ChainCurrent = userData.Chain.Current;
                    ChainMaximum = userData.Chain.Maximum;
                    //ChainTimeout = StatFormatter.FormatFullTime(userData.Chain.Timeout);

                    ChainModifier = userData.Chain.Modifier;
                    ChainCooldown = userData.Chain.Cooldown;
                    ChainPercent =
                                    ChainNextMilestone > ChainPreviousMilestone
                                        ? (double)(ChainCurrent - ChainPreviousMilestone) / (ChainNextMilestone - ChainPreviousMilestone)
                                        : 1.0;
                    /* ChainTimeRemaining = userData.Chain.Cooldown > 0
                        ? StatFormatter.FormatFullTime(userData.Chain.Cooldown)
                        : "READY"; */

                    ChainFormatted = $"{ChainCurrent} / {ChainNextMilestone}";
                }

                MoneyOnhandFormatted = StatFormatter.MoneyOrDash(userData.MoneyOnhand);
                
                DailyNetworthFormatted = StatFormatter.MoneyOrDash(userData.DailyNetworth);
                CaymanBankFormatted = StatFormatter.MoneyOrDash(userData.CaymanBank);
                CityBankAmountFormatted = StatFormatter.MoneyOrDash(userData.CityBank?.Amount);
                CityBankTimeRemaining = StatFormatter.TimeOrDash(userData.CityBank?.Time_Left);

                Points = userData.Points ?? 0;

            }
        }

        partial void OnEnergyCurrentChanged(int value)
        {
            OnPropertyChanged(nameof(EnergyFormatted));
            OnPropertyChanged(nameof(EnergyPercent));
        }

        partial void OnEnergyMaximumChanged(int value)
        {
            OnPropertyChanged(nameof(EnergyFormatted));
            OnPropertyChanged(nameof(EnergyPercent));
        }
        partial void OnNerveCurrentChanged(int value)
        {
            OnPropertyChanged(nameof(NerveFormatted));
            OnPropertyChanged(nameof(NervePercent));
        }
        partial void OnNerveMaximumChanged(int value)
        {
            OnPropertyChanged(nameof(NerveFormatted));
            OnPropertyChanged(nameof(NervePercent));
        }
        partial void OnHappyCurrentChanged(int value)
        {
            OnPropertyChanged(nameof(HappyFormatted));
            OnPropertyChanged(nameof(HappyPercent));
        }
        partial void OnHappyMaximumChanged(int value)
        {
            OnPropertyChanged(nameof(HappyFormatted));
            OnPropertyChanged(nameof(HappyPercent));
        }
        partial void OnLifeCurrentChanged(int value)
        {
            OnPropertyChanged(nameof(LifeFormatted));
            OnPropertyChanged(nameof(LifePercent));
        }
        partial void OnLifeMaximumChanged(int value)
        {
            OnPropertyChanged(nameof(LifeFormatted));
            OnPropertyChanged(nameof(LifePercent));
        }
        partial void OnChainCurrentChanged(int value)
        {
            OnPropertyChanged(nameof(ChainFormatted));
            OnPropertyChanged(nameof(ChainPercent));
            OnPropertyChanged(nameof(ChainNextMilestone));
            OnPropertyChanged(nameof(ChainPreviousMilestone));
        }

        partial void OnChainMaximumChanged(int value)
        {
            OnPropertyChanged(nameof(ChainFormatted));
            OnPropertyChanged(nameof(ChainPercent));
        }

        private void UpdateBarData(
            BarSegment? bar,
            Action<int> setCurrent,
            Action<int> setMax,
            Action<int> setFulltime,
            Action<string> setTimeRemaining,
            Action<string> setFormatted)
        {
            if (bar == null) return;

            setCurrent(bar.Current);
            setMax(bar.Maximum);
            setFulltime(bar.Fulltime);
            setTimeRemaining(StatFormatter.FormatFullTime(bar.Fulltime));
            setFormatted(StatFormatter.FormatStat(bar.Current, bar.Maximum));
        }

    }
}
