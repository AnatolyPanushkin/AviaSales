using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AviaSales.Data.Models
{
    public class Town
    {
        public Town()
        {
            DepartTickets = new HashSet<Ticket>();
            ArriveTickets = new HashSet<Ticket>();
        }
        
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public ICollection<Ticket>? DepartTickets { get; set; }
        public ICollection<Ticket>? ArriveTickets { get; set; }

    }
}