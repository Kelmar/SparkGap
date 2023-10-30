# SparkGap

A logic simulator for digital circuits.

At present the simulation has a single "Pin" component that can be used to set signals of a wire, and several basic logic gates.

It is possible to simulate an RS NOR Latch using the NOR gate.

## TODO
- [ ] Simulation (in progress)
	- [ ] Bus wires (multiple signals down a single pin/wire)
	- [ ] Bidirectional pins.
	- [ ] More built in components
		- [X] Clock
		- [X] AND Gate
		- [X] OR Gate
		- [X] XOR Gate
		- [X] Buffer Gate
		- [X] NOT Gate
		- [X] NOR Gate
		- [ ] NAND Gate
		- [ ] XNOR Gate
		- [ ] Subcircuit (Nested simulations)
	- [ ] Output control of basic gates.  (Allow for tristate output)
- [ ] WPF Drawing of components.
- [ ] WPF Main application.
