using System;
using Newtonsoft.Json;

namespace QuickType
{
    internal class DescriptionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Description) || t == typeof(Description?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == null) return null;
            
            switch (value)
            {
                case "Clear Night":
                    return Description.ClearNight;
                case "Partly Cloudy":
                    return Description.PartlyCloudy;
                case "Partly Cloudy Night":
                    return Description.PartlyCloudyNight;
                case "Cloudy":
                    return Description.Cloudy;
                case "Sunny":
                    return Description.Sunny;
                default:
                    // Fallback for unknown values, logs can be added here if needed
                    // Example: System.Diagnostics.Debug.WriteLine($"Unknown Description: {value}");
                    return Description.Cloudy;
            }
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Description)untypedValue;
            switch (value)
            {
                case Description.ClearNight:
                    serializer.Serialize(writer, "Clear Night");
                    return;
                case Description.Cloudy:
                    serializer.Serialize(writer, "Cloudy");
                    return;
                case Description.PartlyCloudy:
                    serializer.Serialize(writer, "Partly Cloudy");
                    return;
                case Description.PartlyCloudyNight:
                    serializer.Serialize(writer, "Partly Cloudy Night");
                    return;
                case Description.Sunny:
                    serializer.Serialize(writer, "Sunny");
                    return;
            }
            throw new Exception($"Cannot marshal type Description: {value}");
        }

        public static readonly DescriptionConverter Singleton = new DescriptionConverter();
    }
}

