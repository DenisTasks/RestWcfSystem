using System.Runtime.Serialization;

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
