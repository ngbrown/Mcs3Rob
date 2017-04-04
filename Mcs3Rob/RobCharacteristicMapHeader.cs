namespace Mcs3Rob
{
    public class RobCharacteristicMapHeader : RobHeader
    {
        public string Description { get; set; }
        public string Label { get; set; }
        public uint AddressOfMap { get; set; }
        public int Reserve3 { get; set; }
        public int Reserve4 { get; set; }
    }
}