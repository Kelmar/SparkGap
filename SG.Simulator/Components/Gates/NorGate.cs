﻿namespace SG.Simulator.Components.Gates
{
    public class NorGate : BinaryGate
    {
        public NorGate(Circuit circuit)
            : base(circuit, true)
        {
        }

        override protected bool Apply(bool acc, LineState state) => acc && (state == LineState.Low);
    }
}
