using Godot;
using System;
using TaskMaster.Framework;

namespace TaskMaster
{
	public partial class AnalogClock : Control
	{
		private PanelContainer canvasLayer;
		private TextureRect textureRectDrawArea;
		private Timer tickTimer;
		private long processTickCount = 0;

		private void on_TickTimer_timeout()
		{

		}

		public override void _Ready()
		{
			GD.Print("AnalogClock, _Ready() called");
			canvasLayer = GetNodeOrNull<PanelContainer>("PanelBackground");
			Diagnostics.PrintNullValueMessage(canvasLayer, "canvasLayer");
			if (canvasLayer != null)
			{
				textureRectDrawArea = canvasLayer.GetNodeOrNull<TextureRect>("TextureRect");
			}
			tickTimer = new Timer();
			AddChild(tickTimer);
			tickTimer.WaitTime = 0.05f;
			tickTimer.Connect("timeout", new Callable(this, nameof(on_TickTimer_timeout)));
			tickTimer.Start();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			processTickCount += 1;
			if (processTickCount % 20 == 0)
				GD.Print($"AnalogClock, _Process() called, counter = {processTickCount}");
			//textureRectDrawArea.DrawCircle(new Vector2(300.0f, 300.0f), 200.0f, new Color(0.8f, 0.25f, 0.5f, 1.0f));
			//textureRectDrawArea.DrawLine(new Vector2(20.0f, 30.0f), new Vector2(190.0f, 250.0f), new Color(1.0f, 0.75f, 0.45f, 1.0f), 9);
			if (processTickCount % 5 == 1)
				QueueRedraw();
		}

		public void DrawCircleArc(Vector2 center, float radius, float angleFrom, float angleTo, Color color, float width = 1.0f)
		{
			int nbPoints = 64;
			var pointsArc = new Vector2[nbPoints + 1];

			for (int i = 0; i <= nbPoints; i++)
			{
				float anglePoint = Mathf.DegToRad(angleFrom + i * (angleTo - angleFrom) / nbPoints - 90f);
				pointsArc[i] = center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius;
			}

			for (int i = 0; i < nbPoints - 1; i++)
			{
				DrawLine(pointsArc[i], pointsArc[i + 1], color, width);
			}
		}

		private void DrawClockArms(DateTime thisTime, Vector2 centre, float radius, float hourArmW, float minuteArmW, float secondsArmW)
		{
			float angleH = (((float)(thisTime.Hour % 12) / 12.0f) * MathF.PI * 2) - (MathF.PI / 2);
			float angleM = (((float)(thisTime.Minute % 60) / 60.0f) * MathF.PI * 2) - (MathF.PI / 2);
			float angleS = (((float)(thisTime.Second % 60) / 60.0f) * MathF.PI * 2) - (MathF.PI / 2);
			Vector2 edgeVector = centre + new Vector2(Mathf.Cos(angleH), Mathf.Sin(angleH)) * (radius - 70.0f);
			DrawLine(centre, edgeVector, new Color(0.75f, 0.625f, 0.575f, 0.8f), hourArmW);
			edgeVector = centre + new Vector2(Mathf.Cos(angleM), Mathf.Sin(angleM)) * (radius - 45.0f);
			DrawLine(centre, edgeVector, new Color(0.675f, 0.825f, 0.5f, 0.8f), minuteArmW);
			edgeVector = centre + new Vector2(Mathf.Cos(angleS), Mathf.Sin(angleS)) * (radius - 15.0f);
			DrawLine(centre, edgeVector, new Color(0.875f, 0.825f, 0.925f, 0.8f), secondsArmW);
		}

		public override void _Draw()
		{
			//Rect2 myExtent = SceneUtilities.GetApplicationWindowExtent(this);
			Vector2 frameCentre = new Vector2(Size.x / 2, Size.y / 2);
			float radius = Size.y / 2.4f;
			//GD.Print($"AnalogClock, _Draw() called, counter = {processTickCount}");
			DateTime currentTime = DateTime.Now;
			DrawCircle(frameCentre, radius, new Color(0.525f, 0.625f, 0.75f, 0.325f));
			DrawCircleArc(frameCentre, radius, 0.0f, 362.0f, new Color(0.8f, 0.25f, 0.5f, 0.375f), 12.0f);
			DrawClockArms(currentTime, frameCentre, radius, 11.0f, 8.0f, 6.0f);
			//DrawTexture(_texture, new Vector2());			
		}
	}
}
