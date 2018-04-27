using System;
using System.Collections.Generic;
using BLL.EntitesDTO;

namespace BLL.Interfaces
{
    public interface IBLLServiceMain
    {
        IEnumerable<AppointmentDTO> GetAppointmentsByUserId(int id);
        AppointmentDTO GetAppointmentById(int id);
        IEnumerable<AppointmentDTO> GetAppointmentsForCalendar(int id);
        IEnumerable<LocationDTO> GetLocations();
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<AppointmentDTO> GetAppsByLocation(int id);
        IEnumerable<UserDTO> GetAppointmentUsers(int id);
        LocationDTO GetLocationById(int id);
        void AddAppointment(AppointmentDTO appointment, int id);
        AppointmentDTO AddAppointmentWeb(AppointmentDTO appointment, int id);
        AppointmentDTO UpdateAppointment(AppointmentDTO appointment);
        void RemoveFromAppointment(int appointmentId, int userId);
        AppointmentDTO RemoveAppointment(int appointmentId);
        int OverlappingDates(DateTime beginningDate, DateTime endingDate, int locationId);
        int CheckOverlappingDates(DateTime beginningDate, DateTime endingDate, int locationId);
        IEnumerable<AppointmentDTO> GetAppointmentsByUserIdSql(int id);
        IEnumerable<AppointmentDTO> GetAppointmentsByUserIdSqlText(int id, int itemsToSkip, int pageSize);
    }
}
