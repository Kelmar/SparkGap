namespace SG.Simulator.Components.Gates
{
    abstract public class BinaryGate : Component
    {
        private readonly bool m_initAcc;

        protected BinaryGate(Circuit circuit, bool initAcc)
            : base(circuit)
        {
            m_initAcc = initAcc;

            InputA = new Pin(this, PinType.Input);
            InputB = new Pin(this, PinType.Input);

            Output = new Pin(this, PinType.Output);

            Pins.Add(InputA);
            Pins.Add(InputB);

            Pins.Add(Output);
        }

        public Pin InputA { get; }

        public Pin InputB { get; }

        public Pin Output { get; }

        abstract protected bool Apply(bool acc, LineState state);

        public override void Update()
        {
            bool acc = m_initAcc;

            foreach (var input in InputPins)
            {
                LineState state = input.FiniteState;

                if (state == LineState.TooManyDrivers)
                {
                    Output.SetState(LineState.Floating);
                    return;
                }

                acc = Apply(acc, state);
            }

            Output.SetState(acc ? LineState.High : LineState.Low);
        }
    }
}
