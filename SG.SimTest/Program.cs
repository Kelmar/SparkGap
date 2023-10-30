using System;

using SG.Simulator;
using SG.Simulator.Components;

namespace SG.SimTest
{
    internal class Program
    {
        static void ClockTest()
        {
            using var circuit = new Circuit();

            var clock = new Clock(circuit);

            circuit.AddComponent(clock);

            for (int i = 0; i < 10; ++i)
            {
                circuit.Tick();
                Console.WriteLine("Result: {0}", clock.Connection.State);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                ClockTest();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: {0}", ex);
            }
        }
    }
}