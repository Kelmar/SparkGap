using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.Simulator
{
    /// <summary>
    /// Main container of all components and updates.
    /// </summary>
    public class Circuit : IDisposable
    {
        /// <summary>
        /// Holds a list of components that need an update.
        /// </summary>
        private readonly HashSet<Component> m_updates = new();

        /// <summary>
        /// All components in the circuit.
        /// </summary>
        private readonly HashSet<Component> m_components = new();


        virtual protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var c in m_components)
                    c.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Updates once per tick.
        /// </summary>
        /// <remarks>
        /// This can be used by Update() calls to see when the simulation has advanced.
        /// </remarks>
        public int Frame { get; private set; }

        /// <summary>
        /// All components in the circuit.
        /// </summary>
        public IEnumerable<Component> Components => m_components;

        public void AddComponent(Component component)
        {
            m_components.Add(component);
        }

        /// <summary>
        /// Runs a single "tick"
        /// </summary>
        /// <remarks>
        /// We currently consider a tick a frame, which has a start, and multiple 
        /// update cycles until the circuit settles to a fixed state.
        /// </remarks>
        public void Tick()
        {
            ++Frame;

            foreach (var component in m_components)
                component.FrameStart();

            // TODO: Cyclic detection, could be possible to get stuck in an infinite loop here.

            while (m_updates.Any())
            {
                // Realize list as update calls can modify schedules.
                var updates = m_updates.ToList();
                m_updates.Clear();

                foreach (var component in updates)
                    component.Update();
            }
        }

        public void RunTicks(int count)
        {
            for (int i = 0; i < count; ++i)
                Tick();
        }

        public void Schedule(Component component)
        {
            m_updates.Add(component);
        }
    }
}
