using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace MusalaTask
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.EnableSystemDiagnosticsTracing();
            config.Formatters.Add(new BrowserJsonFormatter());
        }

        public class BrowserJsonFormatter : JsonMediaTypeFormatter
        {
            public BrowserJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                this.SerializerSettings.Formatting = Formatting.Indented;
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }

        public class MethodOverrideHandler : DelegatingHandler
        {
            readonly string[] _methods = { "DELETE", "HEAD", "PUT" };
            const string _header = "X-HTTP-Method-Override";

            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                // Check for HTTP POST with the X-HTTP-Method-Override header.
                if (request.Method == HttpMethod.Post && request.Headers.Contains(_header))
                {
                    // Check if the header value is in our methods list.
                    var method = request.Headers.GetValues(_header).FirstOrDefault();
                    if (_methods.Contains(method, StringComparer.InvariantCultureIgnoreCase))
                    {
                        // Change the request method.
                        request.Method = new HttpMethod(method);
                    }
                }
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
