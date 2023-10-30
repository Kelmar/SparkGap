namespace SG.Simulator.Components.Gates
{
    public class AndGate : BinaryGate
    {
        public AndGate(Circuit circuit)
            : base(circuit, true)
        {
        }

        override protected bool Apply(bool acc, LineState state) => acc && (state == LineState.High);
    }
}
