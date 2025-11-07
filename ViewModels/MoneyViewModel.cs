using CommunityToolkit.Mvvm.ComponentModel;
using TornOps.Models;
using TornOps.Helpers;

namespace TornOps.ViewModels
{

    public partial class MoneyViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(OnHandFormatted))]
        private partial long? OnHand { get; set; }
        public string OnHandFormatted => StatFormatter.MoneyOrUnknown(OnHand);

        [ObservableProperty]
        public partial int Points { get; private set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CaymanFormatted))]
        private partial long? Cayman { get; set; }
        public string CaymanFormatted => StatFormatter.MoneyOrUnknown(Cayman);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NetworthFormatted))]
        private partial long? Networth { get; set; }
        public string NetworthFormatted => StatFormatter.MoneyOrUnknown(Networth);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CityBankAmountFormatted))]
        public partial long? CityBankAmount { get; private set; }
        public string CityBankAmountFormatted => StatFormatter.MoneyOrUnknown(CityBankAmount);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CityBankTimeRemaining))]
        [NotifyPropertyChangedFor(nameof(CityBankHasTimer))]
        public partial int? CityBankTimeLeftSeconds { get; private set; }
        public bool CityBankHasTimer => (CityBankTimeLeftSeconds ?? 0) > 0;
        public string CityBankTimeRemaining => StatFormatter.FormatCooldownOrNone(CityBankTimeLeftSeconds);

        public void UpdateFrom(UserDataModel u)
        {
            OnHand = u?.MoneyOnhand;
            Points = u?.Points ?? 0;
            Cayman = u?.CaymanBank;
            Networth = u?.DailyNetworth;
            CityBankAmount = u?.CityBank?.Amount;
            CityBankTimeLeftSeconds = u?.CityBank?.TimeLeft;
        }
    }

}
