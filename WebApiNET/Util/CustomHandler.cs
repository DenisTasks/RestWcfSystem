using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace WebApiNET.Util
{
    public class CustomHandlerWithController : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // здесь можно НЕ СОЗДАВАТЬ контроллер, а сразу вернуть ответ. Парадокс.
            var response = await base.SendAsync(request, cancellationToken);
            response.StatusCode = HttpStatusCode.Created;
            response.Headers.Add("WithController", "TestValue");
            return response;
        }
    }

    public class CustomHandlerWithoutController : DelegatingHandler
    {
        public CustomHandlerWithoutController(HttpConfiguration httpConfiguration)
        {
            // создать внутренний обработчик, т.к. выше он создается с помощью base.SendAsync, которого здесь нет
            InnerHandler = new HttpControllerDispatcher(httpConfiguration);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // здесь МОЖНО вызвать base.SendAsync, т.е. создать контроллер. Всё на совести разработчика
            var response = new HttpResponseMessage(HttpStatusCode.Accepted);
            response.Headers.Add("WithoutController", "TestValue");
            response.StatusCode = HttpStatusCode.Accepted;
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}