using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SG.Utilities;

namespace SG.Simulator
{
    /// <summary>
    /// Represents a single connection point on a component.
    /// </summary>
    public class Pin
    {
        private readonly Component m_component;

        private LineState m_nextState;

        public Pin(Component component, PinType type)
        {
            m_component = component;
            Type = type;
        }

        /// <summary>
        /// The wire this pin is connected to.
        /// </summary>
        public Wire? Wire { get; set; }

        public PinType Type { get; set; }

        public LineState State { get; private set; }

        public LineState FiniteState
        {
            get
            {
                if (State == LineState.Floating)
                    return Rand.FlipCoin() ? LineState.High : LineState.Low;

                return State;
            }
        }

        public void SetState(LineState newState)
        {
            if (m_nextState == LineState.Floating)
                m_nextState = newState;
            else
                m_nextState = LineState.TooManyDrivers;

            Propagate();
        }

        public void Propagate()
        {
            bool send = State != m_nextState;

            State = m_nextState;
            m_nextState = LineState.Floating;

            if (send)
            {
                if (Type == PinType.Input)
                {
                    // Schedule our component for an update.
                    m_component.Schedule();
                }
                else if (Wire != null)
                {
                    // Update the wire.
                    Wire.Update();
                }
            }
        }
    }
}
