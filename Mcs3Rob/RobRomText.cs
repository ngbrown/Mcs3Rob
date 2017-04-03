namespace Mcs3Rob
{
    public class RobRomText : RobDescriptionBlock
    {
        public string Description { get; set; }
        public string Label { get; set; }
        public int Address { get; set; }
        public int TextType { get; set; }
        public int MaxLength { get; set; }
    }
}