using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel;
using BLL.EntitesDTO;

namespace OutlookService.Interfaces
{
    [ServiceContract]
    public interface IOutlookService
    {
        [OperationContract(IsOneWay = false)]
        void Connect();

        [OperationContract(IsOneWay = true)]
        void Disconnect();

        [OperationContract(IsOneWay = true)]
        void Callback();

        //[OperationContract(IsOneWay = false, ProtectionLevel = ProtectionLevel.None)]
        [OperationContract(IsOneWay = false)]
        List<AppointmentDTO> GetAppointments();

        //[OperationContract(IsOneWay = false, ProtectionLevel = ProtectionLevel.Sign)]
        [OperationContract(IsOneWay = false)]

        List<AppointmentDTO> GetAppointmentsWithSql(int id, int itemsToSkip, int pageSize);

        //[OperationContract(IsOneWay = false, ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [OperationContract(IsOneWay = false)]
        AppointmentDTO GetAppointmentById(int id);

        [OperationContract(IsOneWay = false)]
        AppointmentDTO UpdateAppointment(AppointmentDTO appointment);

        [OperationContract(IsOneWay = false)]
        AppointmentDTO RemoveAppointmentById(int id);

        [OperationContract(IsOneWay = false)]
        AppointmentDTO AddAppointment(AppointmentDTO appointment, int id);
    }
}