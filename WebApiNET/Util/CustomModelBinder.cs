using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using BLL.EntitesDTO;

namespace WebApiNET.Util
{
    public class CustomModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(AppointmentDTO))
            {
                return false;
            }
            Trace.WriteLine("bindingContext.ModelName : " + bindingContext.ModelName);
            ValueProviderResult val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (val == null)
            {
                Trace.WriteLine("val is null");
                return false;
            }

            var key = val.RawValue as AppointmentDTO;
            Trace.WriteLine("key val.RawValue " + key);

            AppointmentDTO result = new AppointmentDTO();
            result.Subject = key.Subject;
            result.LocationId = key.LocationId;
            if (result.LocationId > 0)
            {
                bindingContext.Model = result;
                Trace.WriteLine("result.Subject " + result.Subject);
                Trace.WriteLine("result.Location " + result.LocationId);
                return true;
            }

            //if (AppointmentDTO.TryParse(key, out result))
            //{
            //    bindingContext.Model = result;
            //    Trace.WriteLine("result.Subject " + result.Subject);
            //    Trace.WriteLine("result.Location " + result.LocationId);
            //    return true;
            //}
            Trace.WriteLine("IN MODEL FALSE!");
            return false;
        }
    }
}