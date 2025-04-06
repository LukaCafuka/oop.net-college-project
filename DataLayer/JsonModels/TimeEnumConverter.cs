using Newtonsoft.Json;
using QuickType;

public class TimeEnumConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Time);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var value = reader.Value.ToString().Replace("-", "");
        return Enum.TryParse(typeof(Time), value, true, out var result) ? result : Time.FullTime;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString().ToLowerInvariant().Replace("time", "-time"));
    }
}
