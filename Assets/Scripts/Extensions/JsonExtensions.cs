using Newtonsoft.Json;

namespace CheckYourSpeed.Utils
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerSettings _settings = new()
        {
            Converters =
            {
            new IntReactivePropertyJsonConverter()
            }
        };

        public static string ToJson<T>(this T target, bool prettyPrint = false)
        {
            Formatting formatting = prettyPrint
                ? Formatting.Indented
                : Formatting.None;

            return JsonConvert.SerializeObject(target, formatting, _settings);
        }

        public static T FromJson<T>(this string json) =>
            JsonConvert.DeserializeObject<T>(json, _settings);

    }
}
