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

    public class LinkItemProxy
    {

        public LinkItemProxy()
        {
        }

        public void FromLinkItem(LinkItem LinkItem)
        {
            this.ItemType = LinkItem.ItemType;
            this.Place = LinkItem.Place;
            this.Name = LinkItem.Name;
            this.ItemId = (LinkItem.Item as BaseEntity).Id;
        }
        public LinkItem ToLinkItem()
        {
            LinkItem Result = new();
            Result.ItemType = this.ItemType;
            Result.Place = this.Place;
            Result.Name = this.Name;

            switch (ItemType)
            {
                case ItemType.Component:
                    Result.Item = App.CurrentStory.ComponentList.FirstOrDefault(item => item.Id == ItemId);
                    break;
                case ItemType.Chapter:
                    Result.Item = App.CurrentStory.ChapterList.FirstOrDefault(item => item.Id == ItemId);
                    break;
                case ItemType.Scene:
                    Result.Item = App.CurrentStory.SceneList.FirstOrDefault(item => item.Id == ItemId);
                    break;
                case ItemType.Note:
                    Result.Item = App.CurrentStory.NoteList.FirstOrDefault(item => item.Id == ItemId);
                    break;
            }

            return Result;
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
        public string ItemId { get; set; }
    }

    public class LinkItemProxyList
    {
        public LinkItemProxyList() { }
        public List<LinkItemProxy> List { get; set; } = new();
    }
}
