using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class APIClient : IDisposable
    {
        private bool disposed;

        private HttpClient client;
        private HttpResponseMessage hrm;

        public APIClient()
        {
            client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 15);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
        }

        ~APIClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only
                client.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            disposed = true;
        }

        public string RetreiveResponse()
        {
            return hrm.Content.ReadAsStringAsync().GetAwaiter().GetResult(); // not my favorite approach
        }

        public HttpStatusCode IssueGET(string RequestURI)
        {
            HttpStatusCode rv = HttpStatusCode.ServiceUnavailable;

            hrm = client.GetAsync(new Uri(RequestURI)).GetAwaiter().GetResult();
            rv = hrm.StatusCode;

            return rv;
        }
    }
}
