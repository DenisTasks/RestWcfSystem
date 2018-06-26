using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Room { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
