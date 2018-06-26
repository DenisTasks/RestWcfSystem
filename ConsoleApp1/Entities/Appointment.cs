using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string Subject { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }

        public int OrganizerId { get; set; }
        public User Organizer { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
