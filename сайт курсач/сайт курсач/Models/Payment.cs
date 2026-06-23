using System.ComponentModel.DataAnnotations;

namespace сайт_курсач.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public int AppointmentId { get; set; }

        public Appointment? Appointment { get; set; }
    }
}