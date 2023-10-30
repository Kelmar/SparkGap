namespace SG.Simulator.Components.Gates
{
    public class XorGate : BinaryGate
    {
        public XorGate(Circuit circuit)
            : base(circuit, false)
        {
        }

        override protected bool Apply(bool acc, LineState state) => acc ^ (state == LineState.High);
    }
}
