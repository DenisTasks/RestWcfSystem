using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Serialization;
using BLL.EntitesDTO;
using BLL.Interfaces;
using Ninject;
using NLog;
using OutlookService.Interfaces;

namespace OutlookService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, 
                     InstanceContextMode = InstanceContextMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class OutlookService : IOutlookService
    {
        //private readonly IDictionary<string, IOutlookServiceCallback> _callbackDictionary;
        private readonly IList<string> _clientList;
        private readonly ILogger _logger;
        private readonly IKernel _kernel;

        public OutlookService(IKernel kernel)
        {
            _kernel = kernel;
            //_callbackDictionary = new Dictionary<string, IOutlookServiceCallback>();
            _clientList = new List<string>();
            _logger = LogManager.GetCurrentClassLogger();
            _logger.Info(@"Service started");

            var service = _kernel.Get<IBllServiceMain>();
            var appList = service.GetAppointmentsByUserId(1).ToList();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Appointments.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<AppointmentDTO>));
            using (var saver = XmlWriter.Create(path))
            {
                serializer.Serialize(saver, appList);
            }

        }

        public void Connect()
        {
            _logger.Info($"ClientList count : {_clientList.Count}");

            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint =
                prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;

            //var callback = OperationContext.Current.GetCallbackChannel<IOutlookServiceCallback>();
            //var userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;
            try
            {
                if (!_clientList.Contains(ip))
                {
                    _clientList.Add(ip);
                    _logger.Info($"Connected {ip}");
                    foreach (var item in _clientList)
                    {
                        _logger.Info($"In clientList {item}");
                    }
                }
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Appointments.xml");
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                XmlElement xRoot = xDoc.DocumentElement;
                XmlNodeList childnodes = xRoot.SelectNodes("//AppointmentDTO/Subject");
                foreach (XmlNode item in childnodes)
                {
                    //_logger.Info($"I am reading {item.InnerText}");
                }
                //if (callback != null && !_callbackDictionary.ContainsKey("test"))
                //{
                //    _callbackDictionary.Add("test", callback);
                //    _logger.Info(@"User name {0} successfully connected", "test");
                //    foreach (var item in _callbackDictionary)
                //    {
                //        _logger.Info($"In callBackDictionary: {item.Key}");
                //    }
                //}
            }
            catch (SecurityTokenException e)
            {
                throw new FaultException("SECURITY: " + e.Message);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public void Disconnect()
        {
            //var userName = ServiceSecurityContext.Current.PrimaryIdentity.Name;
            //if (_callbackDictionary.ContainsKey(userName))
            //{
            //    _callbackDictionary.Remove(userName);
            //    _logger.Info(@"User with name {0} disconnected", userName);
            //}
        }

        public void Callback()
        {
            //foreach (var item in _callbackDictionary)
            //{
            //    _logger.Trace($"Callback for {item.Key}");
            //    item.Value.OnCallback();
            //}
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public List<AppointmentDTO> GetAppointments()
        {
            var service = _kernel.Get<IBllServiceMain>();
            List<AppointmentDTO> appList;
            try
            {
                appList = service.GetAppointmentsByUserId(1).ToList();
                //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Appointments.xml");
                //XmlSerializer serializer = new XmlSerializer(typeof(List<AppointmentDTO>));
                //using (var reader = XmlReader.Create(path))
                //{
                //    appList = serializer.Deserialize(reader) as List<AppointmentDTO>;
                //}
                _logger.Trace($"Get {appList.Count} appointments from database to Index Page");
            }
            catch (Exception e)
            {
                throw new FaultException("There was a problem connecting to database" + e.Message);
            }
            return appList;
        }

        public List<AppointmentDTO> GetAppointmentsWithSql(int id, int itemsToSkip, int pageSize)
        {
            var service = _kernel.Get<IBllServiceMain>();
            var appList = service.GetAppointmentsByUserIdSqlText(id, itemsToSkip, pageSize).ToList();
            _logger.Trace($"Get {appList.Count} appointments from database to Scrolling Page");
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