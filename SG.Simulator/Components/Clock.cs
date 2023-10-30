using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.Simulator.Components
{
    /// <summary>
    /// Simple clock
    /// </summary>
    /// <remarks>
    /// Right now we cycle each frame and don't actually simulate timing.
    /// </remarks>
    public class Clock : Component
    {
        private bool m_state = false;

        public Clock(Circuit circuit)
            : base(circuit)
        {
            Connection = new Pin(this, PinType.Output);
            Pins.Add(Connection);
        }

        public Pin Connection { get; }

        public override void FrameStart()
        {
            base.FrameStart();

            m_state = !m_state;
            Schedule();
        }

        public override void Update()
        {
            Connection.SetState(m_state ? LineState.High : LineState.Low);
        }
    }
}
