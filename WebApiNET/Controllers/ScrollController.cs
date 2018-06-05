using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.OData;
using System.Windows;
using BLL.EntitesDTO;
using WebApiNET.ServiceReference;
using WebApiNET.Util;

namespace WebApiNET.Controllers
{
    public class ScrollController : ApiController
    {
        private readonly OutlookServiceClient _client;
        private readonly int _pageSize = 10;

        public ScrollController()
        {
            //var callback = new OutlookServiceCallback();
            //var instanceContext = new InstanceContext(callback);
            _client = new OutlookServiceClient();
            try
            {
                _client.Connect();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public HttpResponseMessage GetAppointment(int id)
        {
            var itemsToSkip = id * _pageSize;
            List<AppointmentDTO> collection = new List<AppointmentDTO>();
            try
            {
                collection = _client.GetAppointmentsWithSql(1, itemsToSkip, _pageSize).ToList();
            }
            catch (FaultException e)
            {
                MessageBox.Show(e.Message);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, collection.AsQueryable());

            return response;
        }

        [EnableQuery]
        public HttpResponseMessage GetPage(int page)
        {
            var itemsToSkip = page * _pageSize;
            var collection = _client.GetAppointmentsWithSql(1, itemsToSkip, _pageSize);
            var response = Request.CreateResponse(HttpStatusCode.OK, collection.AsQueryable());

            return response;
        }

        public IHttpActionResult Post([FromBody]AppointmentDTO appointment)
        {
            if (appointment == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppointmentDTO outputMapped = new AppointmentDTO
            {
                BeginningDate = DateTime.Now,
                EndingDate = DateTime.Now.AddHours(1),
                LocationId = 1,
                Subject = appointment.Subject
            };
            var output = _client.AddAppointment(outputMapped, 1);
            if (output != null)
            {
                return Ok(output);
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