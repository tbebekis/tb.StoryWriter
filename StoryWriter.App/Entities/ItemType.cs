namespace StoryWriter
{

    /// <summary>
    /// Indicates the type of an item.
    /// </summary>
    public enum ItemType
    {
        None = 0,
        Group = 1,
        SubGroup = 2,
        Component = 4,
        Chapter = 8,
        Scene = 0x10,
        Tag = 0x20,
    }
}
