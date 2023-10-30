namespace SG.Simulator.Components.Gates
{
    public class OrGate : BinaryGate
    {
        public OrGate(Circuit circuit)
            : base(circuit)
        {
        }

        override protected bool Apply(bool acc, LineState state) => acc || (state == LineState.High);
    }
}
