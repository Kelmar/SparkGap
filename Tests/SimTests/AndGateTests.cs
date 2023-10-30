using NUnit.Framework;

using SG.Simulator;
using SG.Simulator.Components;
using SG.Simulator.Components.Gates;

namespace SimTests
{
    public class AndGateTests
    {
        private Circuit m_circuit;
        private AndGate m_andGate;
        private Wire m_wire;

        [SetUp]
        public void Setup()
        {
            m_circuit = new Circuit();
            m_andGate = new AndGate(m_circuit);
            m_wire = new Wire(m_circuit);

            m_wire.Pins.UnionWith(m_andGate.InputPins);
        }

        [TearDown]
        public void TearDown()
        {
            m_circuit.Dispose();
        }

        [Test]
        public void FloatSettles()
        {
            m_circuit.Tick();

            Assert.That(!m_andGate.Output.State.IsError(), "AND gate shouldn't error");
            Assert.That(m_andGate.Output.State, Is.Not.EqualTo(LineState.Floating), "AND gate shouldn't float.");
        }

        [Test]
        public void GoesHigh()
        {
            var pin = new PinComponent(m_circuit);
            m_wire.Pins.Add(pin.Connection);

            pin.Connection.SetState(LineState.High);

            m_circuit.Tick();

            Assert.That(m_andGate.Output.State, Is.EqualTo(LineState.High));
        }

        [Test]
        public void GoesLow()
        {
            var pin = new PinComponent(m_circuit);
            m_wire.Pins.Add(pin.Connection);

            pin.Connection.SetState(LineState.Low);

            m_circuit.Tick();

            Assert.That(m_andGate.Output.State, Is.EqualTo(LineState.Low));
        }
    }
}
