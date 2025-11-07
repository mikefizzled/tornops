using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;
using TornOps.Helpers;
using TornOps.Models;
using TornOps.Services;
using static System.Net.Mime.MediaTypeNames;

namespace TornOps.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty] private string currentScenario = "user_okay.json";
        public static IReadOnlyList<(string Label, string File)> Scenarios => new[]
        {
        ("Okay",      "user_okay.json"),
        ("Travel",    "user_travel.json"),
        ("Abroad",    "user_abroad.json"),
        ("Hospital",  "user_hospital.json"),
        ("Jail",      "user_jail.json"),
    };
        public IAsyncRelayCommand<string> LoadScenarioCommand { get; }
        public IAsyncRelayCommand RefreshCommand { get; }

        private readonly TornApiService _service = new();

        #region Player Info
        [ObservableProperty] private string playerFormatted;
        [ObservableProperty] private int playerId;
        [ObservableProperty] private string level;
        #endregion

        public StatBarViewModel Energy { get; } = new();
        public StatBarViewModel Nerve { get; } = new();
        public StatBarViewModel Happy { get; } = new();
        public StatBarViewModel Life { get; } = new();
        public ChainViewModel Chain { get; } = new();
        public TravelViewModel Travel { get; } = new();
        public StatusViewModel Status { get; } = new();
        public CooldownsViewModel Cooldowns { get; } = new();
        public MoneyViewModel Money { get; } = new();
        public EducationViewModel Education { get; } = new();
        private readonly EducationCatalogService _eduCatalog = new();

        private bool IsBusy;
        private string ErrorMessage;

        public DashboardViewModel()
        {
            LoadScenarioCommand = new AsyncRelayCommand<string>(LoadScenarioAsync);
            RefreshCommand = new AsyncRelayCommand(() => LoadAsync(CurrentScenario));
        }
        private async Task LoadScenarioAsync(string? assetFile)
        {
            if (string.IsNullOrWhiteSpace(assetFile)) return;
            CurrentScenario = assetFile;
            await LoadAsync(CurrentScenario);
        }
        private async Task LoadAsync(string assetFile)
        {
            IsBusy = true; ErrorMessage = "null";
            try
            {
                var userData = await _service.LoadFromMauiAssetAsync(assetFile);
                if (userData is null) { ErrorMessage = $"No data in {assetFile}."; return; }


                PlayerFormatted = $"{userData.Name} [{userData.PlayerId}]" ?? "??";
                Level = $"Level: {userData.Level}" ?? "??";
                PlayerId = userData.PlayerId ?? 0;

                Energy.UpdateFrom(userData.Energy);
                Nerve.UpdateFrom(userData.Nerve);
                Happy.UpdateFrom(userData.Happy);
                Life.UpdateFrom(userData.Life);
                Chain.UpdateFrom(userData.Chain);
                Travel.UpdateFrom(userData.Travel);
                Status.UpdateFrom(userData.Status);
                Money.UpdateFrom(userData);
                Cooldowns.UpdateFrom(userData.Cooldowns);
                await Education.UpdateFromAsync(userData.EducationUser, _eduCatalog);
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
