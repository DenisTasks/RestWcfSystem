using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BLL.EntitesDTO
{
    [DataContract, Serializable]
    public class AppointmentDTO
    {
        public AppointmentDTO()
        { }

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

        public List<UserDTO> Users { get; set; }
    }
}