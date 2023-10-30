using NUnit.Framework;

using SG.Simulator.Components.Gates;
using SG.Simulator.Components;
using SG.Simulator;

namespace SimTests
{
    public class XorGateTests
    {
        private Circuit m_circuit;
        private XorGate m_xorGate;

        private Wire m_wireA;
        private Wire m_wireB;

        [SetUp]
        public void Setup()
        {
            m_circuit = new Circuit();
            m_xorGate = new XorGate(m_circuit);
            m_wireA = new Wire(m_circuit);
            m_wireB = new Wire(m_circuit);

            m_wireA.Pins.Add(m_xorGate.InputA);
            m_wireB.Pins.Add(m_xorGate.InputB);
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

            Assert.That(!m_xorGate.Output.State.IsError(), "XOR gate shouldn't error");
            Assert.That(m_xorGate.Output.State, Is.Not.EqualTo(LineState.Floating), "XOR gate shouldn't float.");
        }

        [Test]
        public void GoesLow()
        {
            var pinA = new PinComponent(m_circuit);
            var pinB = new PinComponent(m_circuit);

            m_wireA.Pins.Add(pinA.Connection);
            m_wireB.Pins.Add(pinB.Connection);

            pinA.Connection.SetState(LineState.Low);
            pinB.Connection.SetState(LineState.Low);

            m_circuit.Tick();

            Assert.That(m_xorGate.Output.State, Is.EqualTo(LineState.Low));

            pinA.Connection.SetState(LineState.High);
            pinB.Connection.SetState(LineState.High);

            m_circuit.Tick();

            Assert.That(m_xorGate.Output.State, Is.EqualTo(LineState.Low));
        }

        [Test]
        public void GoesHigh()
        {
            var pinA = new PinComponent(m_circuit);
            var pinB = new PinComponent(m_circuit);

            m_wireA.Pins.Add(pinA.Connection);
            m_wireB.Pins.Add(pinB.Connection);

            pinA.Connection.SetState(LineState.High);
            pinB.Connection.SetState(LineState.Low);

            m_circuit.Tick();

            Assert.That(m_xorGate.Output.State, Is.EqualTo(LineState.High));

            pinA.Connection.SetState(LineState.Low);
            pinB.Connection.SetState(LineState.High);

            m_circuit.Tick();

            Assert.That(m_xorGate.Output.State, Is.EqualTo(LineState.High));
        }
    }
}
