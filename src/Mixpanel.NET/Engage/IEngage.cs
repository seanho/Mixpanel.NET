using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mixpanel.NET.Engage
{
    public interface IEngage
    {
        Task<bool> Set(string distinctId, IDictionary<string, object> setProperties);
        Task<bool> Increment(string distinctId, IDictionary<string, object> incrementProperties);
    }
}