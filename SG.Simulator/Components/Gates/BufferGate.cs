using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SG.Simulator.Components.Gates
{
    public class BufferGate : Component
    {
        private readonly Pin m_input;

        public BufferGate(Circuit circuit)
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

            if (state.IsError())
                Output.SetState(LineState.Floating);
            else
            {
                // Mirror the state on our output.
                Output.SetState(state);
            }
        }
    }
}
