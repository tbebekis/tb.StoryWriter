namespace StoryWriter
{

    /// <summary>
    /// Indicates the type of an item.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Flags]
    public enum ItemType
    {
        None = 0,
        Tag = 1,
        Component = 4,
        Chapter = 8,
        Scene = 0x10
    }
}
