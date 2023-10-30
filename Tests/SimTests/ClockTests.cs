using NUnit.Framework;

using SG.Simulator;
using SG.Simulator.Components;

namespace SimTests
{
    public class ClockTests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void BasicTest()
        {
            bool lastState = true;

            using var circuit = new Circuit();
            var clock = new Clock(circuit);

            for (int i = 0; i < 10; ++i)
            {
                circuit.Tick();

                Assert.That(clock.Connection.State, Is.EqualTo(lastState ? LineState.High : LineState.Low));

                lastState = !lastState;
            }
        }
    }
}