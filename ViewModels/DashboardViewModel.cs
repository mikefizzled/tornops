#pragma warning disable MVVMTK0045

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;
using TornOps.Models;
using TornOps.Utils;

namespace TornOps.ViewModels
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

        public StatBarViewModel Energy { get; } = new();
        public StatBarViewModel Nerve { get; } = new();
        public StatBarViewModel Happy { get; } = new();
        public StatBarViewModel Life { get; } = new();
        public ChainViewModel Chain { get; } = new();


        public DashboardViewModel()
        {
            LoadAsync();
        }

        private async void LoadAsync()
        {
            
            var userData = await _service.LoadFromMauiAssetAsync("user.json");

            if (userData != null)
            {
                PlayerFormatted = $"{userData.Name} [{userData.PlayerId}]" ?? "??";
                Level = $"Level: {userData.Level}" ?? "??";
                PlayerId = userData.PlayerId ?? 0;

                if (userData.Status is not null)
                {
                    StatusDescription = userData.Status.Description;
                    StatusState = userData.Status.State;
                    StatusColor = userData.Status.Color;
                }
                Energy.UpdateFrom(userData.Energy);
                Nerve.UpdateFrom(userData.Nerve);
                Happy.UpdateFrom(userData.Happy);
                Life.UpdateFrom(userData.Life);
                Chain.UpdateFrom(userData.Chain);

                MoneyOnhandFormatted = StatFormatter.MoneyOrUnknown(userData.MoneyOnhand);
                
                DailyNetworthFormatted = StatFormatter.MoneyOrUnknown(userData.DailyNetworth);
                CaymanBankFormatted = StatFormatter.MoneyOrUnknown(userData.CaymanBank);
                CityBankAmountFormatted = StatFormatter.MoneyOrUnknown(userData.CityBank?.Amount);
                CityBankTimeRemaining = StatFormatter.FormatCooldownOrNone(userData.CityBank?.Time_Left);

                Points = userData.Points ?? 0;
                DrugCooldownFormatted = StatFormatter.FormatCooldownOrNone(userData.Cooldowns?.DrugCooldown);
            }
        }
    }
}
