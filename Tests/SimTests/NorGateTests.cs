﻿using System;
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
    internal class NorGateTests
    {
        private Circuit m_circuit;
        private NorGate m_norGate;

        private Wire m_wireA;
        private Wire m_wireB;

        [SetUp]
        public void Setup()
        {
            m_circuit = new Circuit();
            m_norGate = new NorGate(m_circuit);
            m_wireA = new Wire(m_circuit);
            m_wireB = new Wire(m_circuit);

            m_wireA.Pins.Add(m_norGate.InputA);
            m_wireB.Pins.Add(m_norGate.InputB);
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

            Assert.That(!m_norGate.Output.State.IsError(), "AND gate shouldn't error");
            Assert.That(m_norGate.Output.State, Is.Not.EqualTo(LineState.Floating), "AND gate shouldn't float.");
        }

        [Test]
        public void GoesHigh()
        {
            var pinA = new PinComponent(m_circuit);
            var pinB = new PinComponent(m_circuit);

            m_wireA.Pins.Add(pinA.Connection);
            m_wireB.Pins.Add(pinB.Connection);

            pinA.Connection.SetState(LineState.Low);
            pinB.Connection.SetState(LineState.Low);

            m_circuit.Tick();

            Assert.That(m_norGate.Output.State, Is.EqualTo(LineState.High));
        }

        [Test]
        public void AndGateGoesLow()
        {
            var pinA = new PinComponent(m_circuit);
            var pinB = new PinComponent(m_circuit);

            m_wireA.Pins.Add(pinA.Connection);
            m_wireB.Pins.Add(pinB.Connection);

            pinA.Connection.SetState(LineState.High);
            pinB.Connection.SetState(LineState.Low);

            m_circuit.Tick();

            Assert.That(m_norGate.Output.State, Is.EqualTo(LineState.Low));

            pinA.Connection.SetState(LineState.Low);
            pinB.Connection.SetState(LineState.High);

            m_circuit.Tick();

            Assert.That(m_norGate.Output.State, Is.EqualTo(LineState.Low));

            pinA.Connection.SetState(LineState.High);

            Assert.That(m_norGate.Output.State, Is.EqualTo(LineState.Low));
        }
    }
}
