using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using SG.Simulator;
using SG.Simulator.Components;
using SG.Simulator.Components.Gates;

namespace SimTests
{
    /// <summary>
    /// Simulate an RS NOR Latch with some NOR gates and test.
    /// </summary>
    public class NorLatchTests
    {
        private Circuit m_circuit;
        private NorGate m_nor1Gate;
        private NorGate m_nor2Gate;

        private Wire m_wireReset;
        private Wire m_wireSet;

        private Wire m_wireQ;
        private Wire m_wireQNot;

        private PinComponent m_pinReset;
        private PinComponent m_pinSet;


        [SetUp]
        public void Setup()
        {
            m_circuit = new Circuit();

            m_nor1Gate = new NorGate(m_circuit);
            m_nor2Gate = new NorGate(m_circuit);

            m_pinReset = new PinComponent(m_circuit);
            m_pinSet = new PinComponent(m_circuit);

            m_wireReset = new Wire(m_circuit);
            m_wireSet = new Wire(m_circuit);

            m_wireQ = new Wire(m_circuit);
            m_wireQNot = new Wire(m_circuit);

            m_wireQ.Pins.Add(m_nor2Gate.Output);
            m_wireQ.Pins.Add(m_nor1Gate.InputB);

            m_wireQNot.Pins.Add(m_nor1Gate.Output);
            m_wireQNot.Pins.Add(m_nor2Gate.InputA);

            m_wireReset.Pins.Add(m_nor1Gate.InputA);
            m_wireSet.Pins.Add(m_nor2Gate.InputB);

            m_wireReset.Pins.Add(m_pinReset.Connection);
            m_wireSet.Pins.Add(m_pinSet.Connection);

            m_pinReset.Connection.SetState(LineState.Low);
            m_pinSet.Connection.SetState(LineState.Low);

            m_circuit.Tick();
        }

        [TearDown]
        public void TearDown()
        {
            m_circuit.Dispose();
        }

        [Test]
        public void FloatSettles()
        {
            Assert.That(!m_nor1Gate.Output.State.IsError(), "Latch should not error.");
            Assert.That(m_nor1Gate.Output.State, Is.Not.EqualTo(LineState.Floating));
        }

        [Test]
        public void ResetsQ()
        {
            if (m_nor1Gate.Output.State != LineState.High)
            {
                m_pinSet.Connection.SetState(LineState.High);
                m_circuit.Tick();

                m_pinSet.Connection.SetState(LineState.Low);
                m_circuit.Tick();
            }

            m_pinReset.Connection.SetState(LineState.High);

            m_circuit.Tick();

            Assert.That(m_nor1Gate.Output.State, Is.EqualTo(LineState.Low));
        }


        [Test]
        public void SetsQ()
        {
            if (m_nor1Gate.Output.State != LineState.Low)
            {
                m_pinReset.Connection.SetState(LineState.High);
                m_circuit.Tick();

                m_pinReset.Connection.SetState(LineState.Low);
                m_circuit.Tick();
            }

            m_pinSet.Connection.SetState(LineState.High);

            m_circuit.Tick();

            Assert.That(m_nor1Gate.Output.State, Is.EqualTo(LineState.High));
        }
    }
}
