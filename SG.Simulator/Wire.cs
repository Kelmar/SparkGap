using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.Simulator
{
    public class Wire : Component
    {
        public Wire(Circuit circuit)
            : base(circuit)
        {
            Pins.CollectionChanged += OnPinsChanged;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
                Pins.CollectionChanged -= OnPinsChanged;
        }

        public LineState State { get; private set; }

        private void OnPinsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
            case NotifyCollectionChangedAction.Add:
                SetPins(e.NewItems?.Cast<Pin>(), State, this);
                break;

            case NotifyCollectionChangedAction.Remove:
                SetPins(e.OldItems?.Cast<Pin>(), LineState.Floating, null);
                break;

            case NotifyCollectionChangedAction.Replace:
                SetPins(e.NewItems?.Cast<Pin>(), State, this);
                SetPins(e.OldItems?.Cast<Pin>(), LineState.Floating, null);
                break;
            }
        }

        private void SetPins(IEnumerable<Pin>? pins, LineState state, Wire? wire)
        {
            if (pins == null)
                return;

            foreach (var pin in pins)
            {
                pin.Wire = wire;

                if (pin.Type == PinType.Input)
                    pin.SetInput(state);
            }
        }

        private void UpdateState()
        {
            var sources =
            (
                from pin in Pins
                where
                    pin.Type != PinType.Input &&
                    pin.State != LineState.Floating &&
                    !pin.State.IsError()
                select pin
            ).ToList();

            if (sources.Count() > 1)
                State = LineState.TooManyDrivers;
            else
                State = sources.FirstOrDefault()?.State ?? LineState.Floating;
        }

        override public void Update()
        {
            UpdateState();

            foreach (var pin in Pins.Where(p => p.Type == PinType.Input))
                pin.SetInput(State);
        }
    }
}
