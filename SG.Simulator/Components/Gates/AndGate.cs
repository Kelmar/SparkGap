namespace SG.Simulator.Components.Gates
{
    public class AndGate : Component
    {
        private readonly Pin m_pinA;
        private readonly Pin m_pinB;

        public AndGate(Circuit circuit)
            : base(circuit)
        {
            m_pinA = new Pin(this, PinType.Input);
            m_pinB = new Pin(this, PinType.Input);

            Output = new Pin(this, PinType.Output);

            Pins.Add(m_pinA);
            Pins.Add(m_pinB);

            Pins.Add(Output);
        }

        public Pin Output { get; }

        public override void Update()
        {
            LineState stateA = m_pinA.FiniteState;
            LineState stateB = m_pinB.FiniteState;

            if (stateA.IsError() || stateB.IsError())
                Output.SetState(LineState.Floating);
            else
            {
                bool val = stateA == LineState.High && stateB == LineState.High;
                Output.SetState(val ? LineState.High : LineState.Low);
            }
        }
    }
}
