using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BLL.EntitesDTO
{
    [DataContract]
    public class AppointmentDTO
    {
        [DataMember]
        public int AppointmentId { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public DateTime BeginningDate { get; set; }
        [DataMember]
        public DateTime EndingDate { get; set; }
        [DataMember]
        public int OrganizerId { get; set; }
        [DataMember]
        public int LocationId { get; set; }
        [DataMember]
        public string Room { get; set; }

        public ICollection<UserDTO> Users { get; set; }

        public static bool TryParse(string s, out AppointmentDTO result)
        {
            result = null;

            string[] parts = s.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }

            string subject;
            int locationId;
            if (int.TryParse(parts[1], out locationId))
            {
                subject = parts[0];
                result = new AppointmentDTO() { LocationId = locationId, Subject = subject };
                return true;
            }
            return false;
        }
    }
}