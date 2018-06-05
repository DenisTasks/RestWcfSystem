using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel;
using BLL.EntitesDTO;

namespace NetTcpService
{
    [ServiceContract]
    public interface IOutlookService
    {
        [OperationContract(IsOneWay = false)]
        int Connect(int a);

        [OperationContract(IsOneWay = true)]
        void Disconnect();

        [OperationContract(IsOneWay = true)]
        void Callback();

        [OperationContract(IsOneWay = false)]
        List<AppointmentDTO> GetAppointments();

        [OperationContract(IsOneWay = false)]

        List<AppointmentDTO> GetAppointmentsWithSql(int id, int itemsToSkip, int pageSize);

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