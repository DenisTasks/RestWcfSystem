using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using BLL.EntitesDTO;

namespace WebApiNET.Util
{
    public class CustomValueProvider : IValueProvider
    {
        private Dictionary<string, string> _values;

        public CustomValueProvider(HttpActionContext actionContext)
        {
            string a = actionContext.Request.RequestUri.AbsoluteUri;
            Trace.WriteLine("In value provider : " + a);

            _values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var header in actionContext.Request.Headers)
            {
                if (header.Key == "AppointmentSubjectHeader")
                {
                    var value = header.Value.ToList().FirstOrDefault();
                    _values["Subject"] = value;
                }
                if (header.Key == "AppointmentLocationHeader")
                {
                    string value = header.Value.ToList().FirstOrDefault();
                    _values["LocationId"] = value;
                }
            }

            Trace.WriteLine("In value provider : ApointmentDTO app successfully created!");
        }

        public bool ContainsPrefix(string prefix)
        {
            return _values.Keys.Contains(prefix);
        }

        public ValueProviderResult GetValue(string key)
        {
            Trace.WriteLine($"In value provider : GetValue key : {key} at {DateTime.Now.TimeOfDay}");
            string value;
            if (_values.TryGetValue(key, out value))
            {
                Trace.WriteLine(value);
                return new ValueProviderResult(value, value, CultureInfo.InvariantCulture);
            }
            return null;
        }
    }
}