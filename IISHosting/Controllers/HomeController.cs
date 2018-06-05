using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IISHosting.Controllers
{
    public class HomeController : ApiController
    {
        public IEnumerable<string> Get()
        {
            var test = Request.Method;
            var test2 = Request.Content;
            var test3 = Request.Headers;
            List<string> testList = new List<string>{test.Method, test2.ToString(), test3.From, test3.Date.ToString()};
            return testList;
        }
    }
}