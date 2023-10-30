namespace SG.Simulator.Components.Gates
{
    public class NotGate : Component
    {
        private readonly Pin m_input;

        public NotGate(Circuit circuit)
            : base(circuit)
        {
            m_input = new Pin(this, PinType.Input);

            Output = new Pin(this, PinType.Output);

            Pins.Add(m_input);
            Pins.Add(Output);
        }

        public Pin Output { get; }

        public override void Update()
        {
            LineState state = m_input.FiniteState;

            if (state.IsError() || state == LineState.Floating)
                Output.SetState(LineState.Floating);
            else
            {
                // Invert the state on our output.
                Output.SetState(state == LineState.Low ? LineState.High : LineState.Low);
            }
        }
    }
}
