using сайт_курсач.Models;

public class Appointment
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public int MasterId { get; set; }
    public Master Master { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public DateTime AppointmentDate { get; set; } // Start

    public string Status { get; set; }

    public string? Notes { get; set; }
}