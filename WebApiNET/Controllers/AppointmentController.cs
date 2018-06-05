using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Windows;
using BLL.EntitesDTO;
using NLog;
using WebApiNET.ServiceReference;
using WebApiNET.Util;
using System.Diagnostics;

namespace WebApiNET.Controllers
{
    public class AppointmentController : ApiController
    {
        private readonly ILogger _logger;
        private readonly OutlookServiceClient _client;

        public AppointmentController()
        {
            _logger = LogManager.GetCurrentClassLogger();
            //var callback = new OutlookServiceCallback();
            //callback.NewCallBack += ExecuteCallBack;
            //var instanceContext = new InstanceContext(callback);
            _client = new OutlookServiceClient();

            //if (_client.ClientCredentials != null)
            //{
            //    _client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
            //        X509CertificateValidationMode.None;
            //    _client.ClientCredentials.UserName.UserName = "testName";
            //    _client.ClientCredentials.UserName.Password = "testPassword";
            //    //_client.ClientCredentials.Windows.ClientCredential.UserName = "testName";
            //    //_client.ClientCredentials.Windows.ClientCredential.Password = "testPassword";
            //    //_client.ClientCredentials.Windows.ClientCredential.Domain = "testDomain";
            //}
            try
            {
                _client.Connect();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ExecuteCallBack(object sender, EventArgs eventArgs)
        {
            //
            // how to call this method from html?
            Get();
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            string clientAddress = HttpContext.Current.Request.UserHostAddress;
            _logger.Info($"Request from {clientAddress}");
            _logger.Info("Getting appointments");
            var collection = new List<AppointmentDTO>();
            try
            {
                collection = _client.GetAppointments().ToList();
            }
            catch (FaultException e)
            {
                MessageBox.Show(e.Message);
            }
            return Ok(collection.AsQueryable());
        }

        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var outputApp = _client.GetAppointmentById(id);
            if (outputApp != null)
            {
                return Ok(outputApp);
            }
            return NotFound();
        }

        public IHttpActionResult Post([FromBody]AppointmentDTO app)
        {
            if (app == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var outputMapped = new AppointmentDTO
            {
                BeginningDate = DateTime.Now,
                EndingDate = DateTime.Now.AddHours(1),
                LocationId = 1,
                Subject = app.Subject
            };
            var appointment = _client.AddAppointment(outputMapped, 1);
            if (appointment != null)
            {
                //_client.Callback();
                return Ok(appointment);
            }
            return Conflict();
        }

        public IHttpActionResult Put([FromBody]AppointmentDTO app)
        {
            if (app == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointment = _client.UpdateAppointment(app);
            if (appointment != null)
            {
                return Ok(appointment);
            }
            return Conflict();
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var appointment = _client.RemoveAppointmentById(id);
            if (appointment != null)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}