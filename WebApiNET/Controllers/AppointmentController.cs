using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.ValueProviders;
using BLL.EntitesDTO;
using WebApiNET.ServiceReference;
using WebApiNET.Util;

namespace WebApiNET.Controllers
{
    public class AppointmentController : ApiController
    {
        private readonly OutlookServiceClient _client;
        private string ip;
        public AppointmentController()
        {
            var callback = new OutlookServiceCallback();
            callback.NewCallBack += ExecuteCallBack;
            var instanceContext = new InstanceContext(callback);
            _client = new OutlookServiceClient(instanceContext);
            try
            {
                _client.Connect(HttpContext.Current.Request.UserHostAddress);
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ExecuteCallBack(object sender, EventArgs eventArgs)
        {
            // how to call this method from html?
            Get();
        }

        [EnableQuery]
        public IQueryable<AppointmentDTO> Get()
        {
            Trace.WriteLine("ip is " + ip);
            var collection = _client.GetAppointments();

            return collection.AsQueryable();
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
            Trace.WriteLine("AppointmentController. Method POST started with subject: " + app.Subject);
            Trace.WriteLine("AppointmentController. Method POST started with locationId: " + app.LocationId);
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
                _client.Callback();
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