namespace TornOps.Models
{
    public class UserDataModel
    {
        public int ServerTime { get; set; }
        public int Level { get; set; }
        public string Gender { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int EducationCurrent { get; set; }
        public int EducationTimeleft { get; set; }
        public int Points { get; set; }
        public long MoneyOnhand { get; set; }
        public long? VaultAmount { get; set; }

        public BarsModel Bars { get; set; }
        public TravelModel Travel { get; set; }
        public EducationUserModel Education { get; set; }
        public CooldownsModel Cooldowns { get; set; }
        public NotificationsModel Notifications { get; set; }
        public StatusModel Status { get; set; }
        public OrganizedCrimeModel OrganizedCrime { get; set; }
    }
}
