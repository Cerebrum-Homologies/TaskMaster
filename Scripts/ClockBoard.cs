using Godot;
using System;
using TaskMaster.Framework;

namespace TaskMaster
{
    public partial class ClockBoard : Control
    {
        private GridContainer gridContainer;

        public override void _Ready()
        {
			GD.Print("ClockBoard, _Ready() called");
			gridContainer = GetNodeOrNull<GridContainer>("GridContainer");
			Diagnostics.PrintNullValueMessage(gridContainer, "gridContainer");
			if (gridContainer != null) {
				CheckButton showAnalogClockButton = new CheckButton();
				gridContainer.AddChild(showAnalogClockButton);
			}
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}