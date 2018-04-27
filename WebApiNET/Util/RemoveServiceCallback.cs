using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiNET.ServiceReference;

namespace WebApiNET.Util
{
    public class OutlookServiceCallback : IOutlookServiceCallback
    {
        public void CallbackEmpty()
        {
        }

        public void CallbackFull(TransferData data)
        {
        }
    }
}