namespace Mcs3Rob
{
    /// <summary>
    /// Description and locations of text strings in ROM/RAM
    /// </summary>
    public class RobRomText : RobDescriptionBlock
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Label, must be unique
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Base ROM address for text storage
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// Text type
        /// </summary>
        public int TextType { get; set; }

        /// <summary>
        /// Max length
        /// </summary>
        public int MaxLength { get; set; }
    }
}