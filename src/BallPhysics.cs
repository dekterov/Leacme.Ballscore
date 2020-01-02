using Godot;
using System;
using System.Linq;
using System.Timers;

public class BallPhysics : RigidBody {

	public override void _Ready() {

		ContactMonitor = true;
		ContactsReported = 10;
	}

	public override void _IntegrateForces(PhysicsDirectBodyState state) {
		if (GetCollidingBodies().Cast<Node>().Any(z => z.Name.Equals("BottomPanePhysics"))) {
			new System.Timers.Timer() { Enabled = true, AutoReset = false, Interval = 2000 }.Elapsed += (z, zz) => { QueueFree(); };
		}
	}

}
