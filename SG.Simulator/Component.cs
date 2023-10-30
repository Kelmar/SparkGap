using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SG.Simulator.Components;
using SG.Utilities;

namespace SG.Simulator
{
    /// <summary>
    /// Represents an electronic component.
    /// </summary>
    /// <remarks>
    /// This can be as simple as a wire or a component pin, or as complex as another sub circuit.
    /// </remarks>
    public abstract class Component : IDisposable
    {
        protected Component(Circuit circuit)
        {
            Circuit = circuit;
            circuit.AddComponent(this);
        }

        virtual protected void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The circuit to which this component is a part of.
        /// </summary>
        public Circuit Circuit { get; }

        public string Label { get; set; } = String.Empty;

        /// <summary>
        /// List of pins
        /// </summary>
        /// <remarks>
        /// Using a set to prevent multiples of the same pin being connected.
        /// </remarks>
        public NotifySet<Pin> Pins { get; } = new();

        /// <summary>
        /// Get a list of all input pins.
        /// </summary>
        public IReadOnlyList<Pin> InputPins => Pins.Where(p => p.Type == PinType.Input).ToList();

        /// <summary>
        /// Get a list of all output pins.
        /// </summary>
        public IReadOnlyList<Pin> OutputPins => Pins.Where(p => p.Type == PinType.Output).ToList();

        /// <summary>
        /// Schedule the component for a deferred update.
        /// </summary>
        public void Schedule()
        {
            Circuit.Schedule(this);
        }

        virtual public void FrameStart()
        {
        }

        abstract public void Update();
    }
}
