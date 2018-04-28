using System;
using System.Collections.Generic;
using BLL.EntitesDTO;

namespace BLL.Interfaces
{
    public interface IBllServiceMain
    {
        IEnumerable<AppointmentDTO> GetAppointmentsByUserId(int id);
        IEnumerable<AppointmentDTO> GetAppointmentsByUserIdSqlText(int id, int itemsToSkip, int pageSize);
        AppointmentDTO GetAppointmentById(int id);
        AppointmentDTO AddAppointment(AppointmentDTO appointment, int id);
        AppointmentDTO UpdateAppointment(AppointmentDTO appointment);
        AppointmentDTO RemoveAppointment(int appointmentId);
    }
}
