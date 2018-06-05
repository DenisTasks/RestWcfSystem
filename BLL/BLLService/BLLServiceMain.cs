using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using BLL.EntitesDTO;
using BLL.Interfaces;
using Model;
using Model.Entities;
using Model.Interfaces;

namespace BLL.BLLService
{
    public class BllServiceMain : IBllServiceMain
    {
        //
        private readonly WPFOutlookContext _context;
        private readonly IGenericRepository<Appointment> _appointments;
        private readonly IGenericRepository<User> _users;
        private readonly IGenericRepository<Location> _locations;

        public BllServiceMain(IGenericRepository<Appointment> appointments, IGenericRepository<User> users, IGenericRepository<Location> locations, WPFOutlookContext context)
        {
            _appointments = appointments;
            _users = users;
            _locations = locations;
            _context = context;
        }

        public IEnumerable<AppointmentDTO> GetAppointmentsByUserId(int id)
        {
            List<Appointment> collection;
            try
            {
                using (_appointments.BeginTransaction())
                {
                    //collection = _appointments.Get(x => x.Users.Any(s => s.UserId == id)).ToList();
                    collection = _appointments.Get().ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            var mappingCollection = Mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentDTO>>(collection).ToList();
            foreach (var item in mappingCollection)
            {
                using (_locations.BeginTransaction())
                {
                    item.Room = _locations.FindById(item.LocationId).Room;
                }
            }
            return mappingCollection;
        }

        public AppointmentDTO GetAppointmentById(int id)
        {
            Appointment appointment;
            using (_appointments.BeginTransaction())
            {
                appointment = _appointments.FindById(id);
            }
            var mappingItem = Mapper.Map<Appointment, AppointmentDTO>(appointment);
            return mappingItem;
        }

        public IEnumerable<AppointmentDTO> GetAppointmentsByUserIdSqlText(int id, int itemsToSkip, int pageSize)
        {
            List<Appointment> collection = new List<Appointment>();
            string connectionString = ConfigurationManager.ConnectionStrings["WPFOutlookContext"].ConnectionString;
            try
            {
                using (var cnn = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM Appointments", cnn))
                    {
                        {
                            cnn.Open();
                            using (SqlDataReader appointmentReader = cmd.ExecuteReader())
                            {
                                while (appointmentReader.Read())
                                {
                                    var getApp = new Appointment
                                    {
                                        AppointmentId = appointmentReader.GetInt32(0),
                                        Subject = appointmentReader.GetString(1),
                                        BeginningDate = appointmentReader.GetDateTime(2),
                                        EndingDate = appointmentReader.GetDateTime(3),
                                        OrganizerId = appointmentReader.GetInt32(4),
                                        LocationId = appointmentReader.GetInt32(5)
                                    };

                                    collection.Add(getApp);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            var mappingCollection = Mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentDTO>>(collection).ToList();
            return mappingCollection.OrderBy(a => a.AppointmentId).Skip(itemsToSkip).Take(pageSize).ToList();
        }

        public AppointmentDTO UpdateAppointment(AppointmentDTO appointment)
        {
            var updateApp = _appointments.FindById(appointment.AppointmentId);
            if (updateApp != null)
            {
                updateApp.Subject = appointment.Subject;
                _appointments.Update(updateApp);
                _context.SaveChanges();
                var outputApp = Mapper.Map<Appointment, AppointmentDTO>(updateApp);
                return outputApp;
            }
            return null;
        }

        public AppointmentDTO AddAppointment(AppointmentDTO appointment, int id)
        {
            var appointmentItem = Mapper.Map<AppointmentDTO, Appointment>(appointment);
            appointmentItem.Organizer = _users.FindById(id);
            appointmentItem.Location = _locations.FindById(appointmentItem.LocationId);
            appointmentItem.OrganizerId = id;
            appointmentItem.Users = new List<User>();
            var convert = Mapper.Map<IEnumerable<UserDTO>, IEnumerable<User>>(appointment.Users);
            foreach (var item in convert)
            {
                if (_users.FindById(item.UserId) != null)
                {
                    appointmentItem.Users.Add(_users.FindById(item.UserId));
                }
            }

            using (var transaction = _appointments.BeginTransaction())
            {
                try
                {
                    _appointments.Create(appointmentItem);
                    _appointments.Save();
                    var outputItem = _appointments.Get().LastOrDefault();
                    transaction.Commit();
                    return Mapper.Map<Appointment, AppointmentDTO>(outputItem);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.StackTrace);
                }
            }
        }

        public AppointmentDTO RemoveAppointment(int appointmentId)
        {
            var appointment = _appointments.FindById(appointmentId);
            if (appointment != null)
            {
                using (var transaction = _appointments.BeginTransaction())
                {
                    try
                    {
                        _appointments.Remove(appointment);
                        _appointments.Save();
                        transaction.Commit();
                        return Mapper.Map<Appointment, AppointmentDTO>(appointment);
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e + " from BLL");
                    }
                }
            }
            return null;
        }
    }
}