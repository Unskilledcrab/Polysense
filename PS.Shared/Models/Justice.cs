namespace PS.Shared.Models
{
    public class Justice : Judge
    {
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public Politician AppointedBy { get; set; }
    }
}