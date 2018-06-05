using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            var selfHostConfiguraiton = new HttpSelfHostConfiguration("http://localhost:52644");

            selfHostConfiguraiton.Routes.MapHttpRoute(
                name: "DefaultApiRoute",
                routeTemplate: "api/{controller}",
                defaults: null
            );

            using (var server = new HttpSelfHostServer(selfHostConfiguraiton))
            {
                server.OpenAsync().Wait();
                Console.ReadLine();
            }
        }
    }
}
