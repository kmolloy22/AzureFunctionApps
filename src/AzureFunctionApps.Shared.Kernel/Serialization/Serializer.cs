using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AzureFunctionApps.Shared.Kernel.Serialization
{
    public static class Serializer
    {
        public static string ToJson(object subject, bool ignoreNullValues = true, bool ignoreDefaultValues = false, bool writeIndented = false, bool useCamelCase = true)
        {
            var options = new JsonSerializerSettings();

            if (ignoreNullValues)
                options.NullValueHandling = NullValueHandling.Ignore;

            if (ignoreDefaultValues)
                options.DefaultValueHandling = DefaultValueHandling.Ignore;

            if (writeIndented)
                options.Formatting = Formatting.Indented;

            if (useCamelCase)
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(subject, options);
        }

        public static T ToModel<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}