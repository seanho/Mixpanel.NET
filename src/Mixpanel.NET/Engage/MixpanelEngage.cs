using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mixpanel.NET.Engage
{
    public class MixpanelEngage : MixpanelClientBase, IEngage
    {
        private readonly EngageOptions _options;

        /// <summary>
        /// Creates a new Mixpanel Engage client for a given API token
        /// </summary>
        /// <param name="token">The API token for MixPanel</param>
        /// <param name="http">Optional: An implementation of IMixpanelHttp, <see cref="MixpanelHttp"/>
        /// Determines if class names and properties will be serialized to JSON literally.
        /// If false (the default) spaces will be inserted between camel-cased words for improved 
        /// readability on the reporting side.
        /// </param>
        /// <param name="options">Optional: Specific options for the API <see cref="EngageOptions"/></param>
        public MixpanelEngage(string token, IMixpanelHttp http = null, EngageOptions options = null)
            : base(token, http)
        {
            _options = options ?? new EngageOptions();
        }

        private async Task<bool> Engage(string distinctId, IDictionary<string, object> setProperties = null,
          IDictionary<string, object> incrementProperties = null)
        {
            // Standardize token and time values for Mixpanel
            var dictionary =
              new Dictionary<string, object> { { "$token", token }, { "$distinct_id", distinctId } };

            if (setProperties != null) dictionary.Add("$set", setProperties);

            if (incrementProperties != null) dictionary.Add("$add", incrementProperties);

            var data = JsonConvert.SerializeObject(dictionary);

            var values = "data=" + data.Base64Encode();

            string contents = null;
            if (_options.UseGet)
            {
                contents = await http.Get(Resources.Engage(_options.ProxyUrl), values); 
            }
            else
            {
                contents = await http.Post(Resources.Engage(_options.ProxyUrl), values);
            }

            return contents == "1";
        }

        public Task<bool> Set(string distinctId, IDictionary<string, object> setProperties)
        {
            return Engage(distinctId, setProperties);
        }

        public Task<bool> Increment(string distinctId, IDictionary<string, object> incrementProperties)
        {
            return Engage(distinctId, incrementProperties: incrementProperties);
        }
    }
}