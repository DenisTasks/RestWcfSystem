using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OutlookService.DTOs
{
    [DataContract]
    public class TransferData
    {
        [DataMember]
        public bool BoolValue { get; set; }
        [DataMember]
        public string StringValue { get; set; }
    }
}
