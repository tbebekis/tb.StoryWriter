namespace StoryWriter
{

    /// <summary>
    /// A link in a body text.
    /// <para>Indicates the item it points to using the <see cref="ItemType"/> and the <see cref="ItemId"/></para>
    /// </summary>
    public class LinkItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LinkItem() 
        { 
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public LinkItem(ItemType Type, LinkPlace Place, string Name, object Item)
        {
            this.Name = Name;
            this.Place = Place;
            this.Item = Item;
            this.ItemType = Type;
        }

        public override string ToString()
        {
            return $"{ItemType} - {Name}";
        }


        /// <summary>
        /// Indicates the type of a link, i.e. group, subgroup, component, chapter, scene, tag
        /// <para>Valid values are component and chapter. </para>
        /// </summary>
        public ItemType ItemType { get; set; } 
        public LinkPlace Place { get; set; } // title or text
        public string Name { get; set; }
        /// <summary>
        /// The item
        /// </summary>
        public object Item { get; set; }
    }
}
