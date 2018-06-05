using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using BLL.EntitesDTO;
using BLL.Interfaces;
using Ninject;

namespace NetTcpService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, 
                     InstanceContextMode = InstanceContextMode.Single)]
    public class OutlookService : IOutlookService
    {
        public int Connect(int a)
        {
            return a * 10;
        }

        public void Disconnect()
        {

        }

        public void Callback()
        {

        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public List<AppointmentDTO> GetAppointments()
        {
            List<AppointmentDTO> appList = new List<AppointmentDTO>();
            try
            {
            }
            catch (Exception e)
            {
                throw new FaultException("There was a problem connecting to database" + e.Message);
            }
            return appList;
        }

        public List<AppointmentDTO> GetAppointmentsWithSql(int id, int itemsToSkip, int pageSize)
        {
            List<AppointmentDTO> appList = new List<AppointmentDTO>();

            return appList;
        }

        public AppointmentDTO UpdateAppointment(AppointmentDTO updateApp)
        {
            AppointmentDTO appointment = new AppointmentDTO();
            if (appointment != null)
            {
                return appointment;
            }
            return null;
        }

        public AppointmentDTO GetAppointmentById(int id)
        {
            try
            {
                AppointmentDTO appointment = new AppointmentDTO();
                if (appointment != null)
                {
                    return appointment;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return null;
        }

        public AppointmentDTO RemoveAppointmentById(int id)
        {
            try
            {
                AppointmentDTO appointment = new AppointmentDTO();
                if (appointment != null)
                {
                    return appointment;
                }
            }
            catch (Exception e)
            {
                // ignored
            }
            return null;
        }

        public AppointmentDTO AddAppointment(AppointmentDTO addApp, int id)
        {
            try
            {
                AppointmentDTO appointment = new AppointmentDTO();
                if (appointment != null)
                {
                    return appointment;
                }
            }
            catch (Exception e)
            {
                // ignored
            }
            return null;
        }
    }
}