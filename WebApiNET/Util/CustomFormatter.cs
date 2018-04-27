using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Model.Entities;

namespace WebApiNET.Util
{
    public class CustomFormatter : MediaTypeFormatter
    {
        public CustomFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-appointments"));
        }
        public override bool CanReadType(Type type)
        {
            return type == typeof(Appointment) || type == typeof(IEnumerable<Appointment>);
        }
        public override bool CanWriteType(Type type)
        {
            return type == typeof(Appointment) || type == typeof(IEnumerable<Appointment>);
        }
        public override async Task WriteToStreamAsync(Type type, object value,
            Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            List<string> appsString = new List<string>();
            IEnumerable<Appointment> apps;
            if (value is Appointment)
            {
                apps = new[] {(Appointment)value};
            }
            else
            {
                apps = (IEnumerable<Appointment>)value;
            }
            foreach (var b in apps)
            {
                appsString.Add($"This is appointment {b.AppointmentId}");
            }
            StreamWriter writer = new StreamWriter(writeStream);
            await writer.WriteAsync(string.Join(",", appsString));
            writer.Flush();
        }
    }
}