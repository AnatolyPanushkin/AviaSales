using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AviaSales.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        
        public int TicketNumber { get; set; }
        public virtual Town? DepartTown { get; set; }
        public virtual Town? ArriveTown { get; set; }
        public virtual Passenger? Passenger { get; set; }
    }
}