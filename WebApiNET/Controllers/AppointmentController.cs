using System;
using System.Linq;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.OData;
using BLL.EntitesDTO;
using WebApiNET.ServiceReference;
using WebApiNET.Util;

namespace WebApiNET.Controllers
{
    public class AppointmentController : ApiController
    {
        private readonly OutlookServiceClient _client;

        public AppointmentController()
        {
            var callback = new OutlookServiceCallback();
            var instanceContext = new InstanceContext(callback);
            _client = new OutlookServiceClient(instanceContext);
            try
            {
                _client.Connect(1);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [EnableQuery]
        public IQueryable<AppointmentDTO> Get()
        {
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