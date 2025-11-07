// TornOps.ViewModels/CooldownsViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using TornOps.Helpers;
using TornOps.Models;

namespace TornOps.ViewModels;

public partial class CooldownsViewModel : ObservableObject
{
    // DRUG
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DrugFormatted))]
    [NotifyPropertyChangedFor(nameof(DrugIconPath))]
    public partial int DrugSeconds { get; private set; }

    public string DrugFormatted => StatFormatter.FormatCooldownOrNone(DrugSeconds);
    public string DrugIconPath => Icons.DrugIcon(DrugSeconds);

    // MEDICAL
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MedicalFormatted))]
    [NotifyPropertyChangedFor(nameof(MedicalIconPath))]
    public partial int MedicalSeconds { get; private set; }

    public string MedicalFormatted => StatFormatter.FormatCooldownOrNone(MedicalSeconds);
    public string MedicalIconPath => Icons.MedicalIcon(MedicalSeconds);

    // BOOSTER
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BoosterFormatted))]
    [NotifyPropertyChangedFor(nameof(BoosterIconPath))]
    public partial int BoosterSeconds { get; private set; }

    public string BoosterFormatted => StatFormatter.FormatCooldownOrNone(BoosterSeconds);
    public bool BoosterHasCooldown => BoosterSeconds > 0;
    public string BoosterIconPath => Icons.BoosterIcon(BoosterSeconds);

    public void UpdateFrom(CooldownsModel? c)
    {
        DrugSeconds = c?.DrugCooldown ?? 0;
        MedicalSeconds = c?.MedicalCooldown ?? 0;
        BoosterSeconds = c?.BoosterCooldown ?? 0;
    }
}
