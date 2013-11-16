using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mixpanel.NET.Events
{
    public interface IEventTracker
    {
        Task<bool> Track(string @event, IDictionary<string, object> properties);
        Task<bool> Track(MixpanelEvent @event);
        Task<bool> Track<T>(T @event);
    }
}