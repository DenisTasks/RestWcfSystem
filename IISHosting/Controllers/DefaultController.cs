using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using IISHosting.SSL;

namespace IISHosting.Controllers
{
    public class DefaultController : ApiController
    {
        //[UseSSL]
        public async Task<IHttpActionResult> Get()
        {
            var datetime = DateTimeOffset.Now;
            await Task.Factory.StartNew(() =>
            {
                datetime = DateTimeOffset.Now;
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(20000);
                    //using (System.IO.File.Create($@"C:\Temp\{DateTime.Now.Second}.txt")) ;
                });
            });
            
            return Ok(datetime);
        }

        // GET: api/Default/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}