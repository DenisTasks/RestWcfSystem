using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using BLL.EntitesDTO;
using BLL.Interfaces;
using Ninject;
using NLog;
using OutlookService.Interfaces;

namespace OutlookService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, 
                     InstanceContextMode = InstanceContextMode.Single)]
    public class OutlookService : IOutlookService
    {
        private readonly IDictionary<int, IOutlookServiceCallback> _callbackDictionary;
        private readonly ILogger _logger;
        private readonly IKernel _kernel;

        public OutlookService(IKernel kernel)
        {
            _kernel = kernel;
            _callbackDictionary = new Dictionary<int, IOutlookServiceCallback>();
            _logger = LogManager.GetCurrentClassLogger();
            _logger.Info(@"Service started");
        }

        public void Connect(int id)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IOutlookServiceCallback>();
            if (callback != null && !_callbackDictionary.ContainsKey(id))
            {
                _callbackDictionary.Add(id, callback);
                _logger.Info(@"User with ID {0} and name {1} successfully connected", id, System.Threading.Thread.CurrentPrincipal.Identity.Name);
                foreach (var item in _callbackDictionary)
                {
                    _logger.Info($"In callBackDictionary: {item.Key}");
                }
            }
        }

        public void Disconnect(int id)
        {
            if (_callbackDictionary.ContainsKey(id))
            {
                _callbackDictionary.Remove(id);
                _logger.Info(@"User with ID {0} and name {1} disconnected", id, System.Threading.Thread.CurrentPrincipal.Identity.Name);
            }
        }

        public void Callback()
        {
            foreach (var item in _callbackDictionary)
            {
                _logger.Trace($"Callback for {item.Key}");
                item.Value.OnCallback();
            }
        }

        public List<AppointmentDTO> GetAppointments()
        {
            var service = _kernel.Get<IBllServiceMain>();
            var appList = service.GetAppointmentsByUserId(1).ToList();
            foreach (var item in appList)
            {
                _logger.Trace($"Get appointment with ID {item.AppointmentId} from database to Index Page");
            }
            return appList;
        }

        public List<AppointmentDTO> GetAppointmentsWithSql(int id, int itemsToSkip, int pageSize)
        {
            var service = _kernel.Get<IBllServiceMain>();
            var appList = service.GetAppointmentsByUserIdSqlText(id, itemsToSkip, pageSize).ToList();
            foreach (var item in appList)
            {
                _logger.Trace($"Get appointment with ID {item.AppointmentId} from database to Scrolling Page");
            }
            return appList;
        }

        public AppointmentDTO UpdateAppointment(AppointmentDTO updateApp)
        {
            var service = _kernel.Get<IBllServiceMain>();
            var appointment = service.UpdateAppointment(updateApp);
            if (appointment != null)
            {
                _logger.Trace($"Appointment with ID {appointment.AppointmentId} updated sucessfully");
                return appointment;
            }
            return null;
        }

        public AppointmentDTO GetAppointmentById(int id)
        {
            try
            {
                var service = _kernel.Get<IBllServiceMain>();
                var appointment = service.GetAppointmentById(id);
                if (appointment != null)
                {
                    _logger.Trace($"Get appointment with ID {appointment.AppointmentId} from database");
                    return appointment;
                }
            }
            catch (Exception e)
            {
                _logger.Fatal(e.StackTrace);
            }
            return null;
        }

        public AppointmentDTO RemoveAppointmentById(int id)
        {
            try
            {
                var service = _kernel.Get<IBllServiceMain>();
                var appointment = service.RemoveAppointment(id);
                if (appointment != null)
                {
                    _logger.Trace($"Remove appointment with ID {appointment.AppointmentId} successfully");
                    return appointment;
                }
            }
            catch (Exception e)
            {
                _logger.Fatal(e.StackTrace);
            }
            return null;
        }

        public AppointmentDTO AddAppointment(AppointmentDTO addApp, int id)
        {
            try
            {
                var service = _kernel.Get<IBllServiceMain>();
                var appointment = service.AddAppointment(addApp, id);
                if (appointment != null)
                {
                    _logger.Trace($"Create appointment with ID {appointment.AppointmentId} successfully");
                    return appointment;
                }
            }
            catch (Exception e)
            {
                _logger.Fatal(e.StackTrace);
            }
            return null;
        }
    }
}