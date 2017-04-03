namespace Mcs3Rob
{
    public class RobCharSpace : RobCharacteristicMap
    {
        public RobIndependantAxis UAxis { get; set; }
        public RobIndependantAxis VAxis { get; set; }
        public RobIndependantAxis WAxis { get; set; }
        public RobDependantAxis QAxis { get; set; }
    }
}