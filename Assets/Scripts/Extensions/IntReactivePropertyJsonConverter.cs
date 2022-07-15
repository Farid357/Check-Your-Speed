using Newtonsoft.Json;
using System;
using UniRx;

namespace CheckYourSpeed.Utils
{
    public class IntReactivePropertyJsonConverter : JsonConverter<IntReactiveProperty>
    {
        public override void WriteJson(JsonWriter writer, IntReactiveProperty value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }

        public override IntReactiveProperty ReadJson(
            JsonReader reader,
            Type objectType,
            IntReactiveProperty existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {

            object readerValue = reader.Value;
            var s = readerValue.ToString();
            int value = int.Parse(s);

            return new IntReactiveProperty(value);
        }
    }
}
