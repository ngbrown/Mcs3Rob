namespace Mcs3Rob
{
    public class RobCharacteristicMapHeader : RobHeader
    {
        public string Description { get; set; }
        public string Label { get; set; }
        public int AddressOfMap { get; set; }
        public int Reserve1 { get; set; }
        public int Reserve2 { get; set; }
    }
}