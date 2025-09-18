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


        /// <summary>
        /// Returns true if any of this instance body text contains a specified term.
        /// </summary>
        public virtual bool BodyTextContainsTerm(string Term, bool WholeWordOnly)
        {
            return false;
        }
        /// <summary>
        /// Returns true if any of this instance name contains a specified term.
        /// </summary>
        public virtual bool NameContainsTerm(string Term, bool WholeWordOnly)
        {
            return WholeWordOnly ? App.ContainsWord(Name, Term) : Name.ContainsText(Term);
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
