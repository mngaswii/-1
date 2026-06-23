namespace сайт_курсач.Models
{
    public class Service
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public decimal Price { get; set; }

        
        public int DurationMinutes { get; set; }

      
        public List<Appointment>? Appointments { get; set; }
    }
}