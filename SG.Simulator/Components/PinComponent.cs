namespace SG.Simulator.Components
{
    /// <summary>
    /// A component that is a pin in of itself.
    /// </summary>
    public class PinComponent : Component
    {
        public PinComponent(Circuit circuit)
            : base(circuit)
        {
            Connection = new Pin(this, PinType.Output);
            Pins.Add(Connection);
        }

        public Pin Connection { get; }

        public override void Update()
        {
        }
    }
}
