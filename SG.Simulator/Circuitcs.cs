using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.Simulator
{
    public class Circuit
    {
        private readonly HashSet<Component> m_updates = new HashSet<Component>();

        public void Tick()
        {
            while (m_updates.Any())
            {
                // Realize list as update calls can modify schedules.
                var updates = m_updates.ToList();
                m_updates.Clear();

                foreach (var c in updates)
                    c.Update();
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
