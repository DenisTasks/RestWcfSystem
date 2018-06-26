using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    [DataContract]
    public class Appointment
    {
        [DataMember, Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        [DataMember, Required]
        public string Subject { get; set; }
        [DataMember]
        public DateTime BeginningDate { get; set; }
        [DataMember]
        public DateTime EndingDate { get; set; }

        public int OrganizerId { get; set; }
        public User Organizer { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
