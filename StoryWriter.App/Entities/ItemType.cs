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
        ComponentType = 2,
        Component = 4,
        Chapter = 8,
        Scene = 0x10,
        Note = 0x20,
        TagToComponent = 0x40,
        ComponentToScene = 0x80
    }
}
