using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHosting
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public IEnumerable<int> Get()
        {
            List<int> testList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            return testList;
        }
    }
}
