namespace StoryWriter
{
    public class BaseEntity
    {
        // ● public
        /// <summary>
        /// Returns the name of this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        
        // ● properties
        /// <summary>
        /// The unique identifier of this instance.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The name of this instance. Must be unique across all instances.
        /// </summary>
        public string Name { get; set; }
    }
}
