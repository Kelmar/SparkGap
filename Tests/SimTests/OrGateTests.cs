using NUnit.Framework;
using SG.Simulator.Components.Gates;
using SG.Simulator.Components;
using SG.Simulator;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTests
{
    public class OrGateTests
    {
        private Circuit m_circuit;
        private OrGate m_orGate;

        private Wire m_wireA;
        private Wire m_wireB;

        [SetUp]
        public void Setup()
        {
            m_circuit = new Circuit();
            m_orGate = new OrGate(m_circuit);
            m_wireA = new Wire(m_circuit);
            m_wireB = new Wire(m_circuit);

            m_wireA.Pins.Add(m_orGate.InputA);
            m_wireB.Pins.Add(m_orGate.InputB);
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

            Assert.That(!m_orGate.Output.State.IsError(), "AND gate shouldn't error");
            Assert.That(m_orGate.Output.State, Is.Not.EqualTo(LineState.Floating), "AND gate shouldn't float.");
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

            Assert.That(m_orGate.Output.State, Is.EqualTo(LineState.Low));
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

            Assert.That(m_orGate.Output.State, Is.EqualTo(LineState.High));

            pinA.Connection.SetState(LineState.Low);
            pinB.Connection.SetState(LineState.High);

            m_circuit.Tick();

            Assert.That(m_orGate.Output.State, Is.EqualTo(LineState.High));

            pinA.Connection.SetState(LineState.High);

            Assert.That(m_orGate.Output.State, Is.EqualTo(LineState.High));
        }
    }
}
