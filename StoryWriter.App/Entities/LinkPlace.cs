namespace StoryWriter
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LinkPlace
    {
        Title = 0,
        Text = 1,
    }
}
