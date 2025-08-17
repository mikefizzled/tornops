namespace TornOps.Models
{
    public class ChainModel
    {
        public int Current { get; set; }
        public int Maximum { get; set; }
        public int Timeout { get; set; }
        public int Modifier { get; set; }
        public int Cooldown { get; set; }
    }
}