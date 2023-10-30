using NUnit.Framework;

using SG.Simulator.Components.Gates;
using SG.Simulator.Components;
using SG.Simulator;
using System.Linq;

namespace SimTests
{
    internal class BufferGateTests
    {
        private Circuit m_circuit;
        private BufferGate m_bufGate;

        private Wire m_wire;

        [SetUp]
        public void Setup()
        {
            m_circuit = new Circuit();
            m_bufGate = new BufferGate(m_circuit);
            m_wire = new Wire(m_circuit);

            m_wire.Pins.Add(m_bufGate.InputPins.Single());
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

            Assert.That(!m_bufGate.Output.State.IsError(), "Buffer gate shouldn't error");
            Assert.That(m_bufGate.Output.State, Is.Not.EqualTo(LineState.Floating), "Buffer gate shouldn't float.");
        }

        [Test]
        public void GoesLow()
        {
            var pin = new PinComponent(m_circuit);

            m_wire.Pins.Add(pin.Connection);

            pin.Connection.SetState(LineState.Low);

            m_circuit.Tick();

            Assert.That(m_bufGate.Output.State, Is.EqualTo(LineState.Low));
        }

        [Test]
        public void GoesHigh()
        {
            var pin = new PinComponent(m_circuit);

            m_wire.Pins.Add(pin.Connection);

            pin.Connection.SetState(LineState.High);

            m_circuit.Tick();

            Assert.That(m_bufGate.Output.State, Is.EqualTo(LineState.High));
        }
    }
}
