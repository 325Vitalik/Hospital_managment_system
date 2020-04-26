namespace DataBase.Models
{
    public class Drug
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
    }
}