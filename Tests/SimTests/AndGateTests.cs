using System.Linq;

using NUnit.Framework;

using SG.Simulator;
using SG.Simulator.Components.Gates;

namespace SimTests
{
    public class AndGateTests
    {
        [Test]
        public void AndGateFloatSettles()
        {
            using var circuit = new Circuit();

            var andGate = new AndGate(circuit);

            var wire = new Wire(circuit);

            wire.Pins.Union(andGate.InputPins);

            circuit.Tick();

            Assert.That(!andGate.Output.State.IsError(), "AND gate shouldn't error");
            Assert.That(andGate.Output.State, Is.Not.EqualTo(LineState.Floating), "AND gate shouldn't float.");
        }
    }
}
