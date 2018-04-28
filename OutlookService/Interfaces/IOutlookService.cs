using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using BLL.EntitesDTO;

namespace OutlookService.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IOutlookServiceCallback))]
    public interface IOutlookService
    {
        [OperationContract(IsOneWay = true)]
        void Connect(int id);

        [OperationContract(IsOneWay = true)]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void Callback(int id);

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