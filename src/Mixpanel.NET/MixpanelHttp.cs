using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mixpanel.NET
{
    /// <summary>
    /// This helper class and interface largly exists to improve readability and testability since there is 
    /// no way to do that with the WebRequest class cleanly.
    /// </summary>
    public interface IMixpanelHttp
    {
        Task<string> Get(string uri, string query);
        Task<string> Post(string uri, string body);
    }

    public class MixpanelHttp : IMixpanelHttp
    {
        public async Task<string> Get(string uri, string query)
        {
            return await new HttpClient().GetStringAsync(uri + "?" + query);
        }

        public async Task<string> Post(string uri, string body)
        {
            var content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            var result = await new HttpClient().PostAsync(uri, content);
            return await result.Content.ReadAsStringAsync();
        }
    }
}