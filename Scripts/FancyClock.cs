using Godot;
using System;
using TaskMaster.Framework;

namespace TaskMaster
{
	public partial class FancyClock : Control
	{
		private Sprite2D backgroundImage;
		private long processTickCount = 0;

		public override void _Ready()
		{
			GD.Print("FancyClock, _Ready() called");
			backgroundImage = GetNodeOrNull<Sprite2D>("Sprite2D-BackFrame");
			Diagnostics.PrintNullValueMessage(backgroundImage, "backgroundImage");
			if (backgroundImage != null)
			{
				Vector2 bgSize = backgroundImage.Texture.GetSize();
				backgroundImage.Scale = new Vector2(Size.x / bgSize.x, Size.y / bgSize.y);
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			processTickCount += 1;
			if (processTickCount % 20 == 0)
				GD.Print($"FancyClock, _Process() called, counter = {processTickCount}");
			if (processTickCount % 5 == 1)
				QueueRedraw();
		}

		private float GetCharactersDrawnWidth(Font mainFont, String textDrawn, int fontHeight)
		{
			float res = 0;
			if (textDrawn.Length > 0)
			{
				Vector2 stringSize = mainFont.GetStringSize(textDrawn, HorizontalAlignment.Left, -1, fontHeight);
				res = stringSize.x;
			}
			return res;
		}

		private Vector2 GetCharactersDrawnSize(Font mainFont, String textDrawn, int fontHeight)
		{
			Vector2 res = Vector2.Zero;
			if (textDrawn.Length > 0)
			{
				Vector2 stringSize = mainFont.GetStringSize(textDrawn, HorizontalAlignment.Left, -1, fontHeight);
				res = stringSize;
			}
			return res;
		}

		public override void _Draw()
		{
			DateTime currentTime = DateTime.Now;
			Vector2 frameCentre = new Vector2(Size.x / 2, Size.y / 2);
			float radius = Size.y / 6.4f;
			DrawCircle(frameCentre, radius, new Color(0.325f, 0.675f, 0.785f, 0.275f));
			SystemFont drawFont = new SystemFont();
			drawFont.FontNames = new string[] { "Roman", "Courier"};
			drawFont.FontWeight = 500;
			int fontHeight = (int)(Size.y * 0.765f);
			//float topLineY = 25.0f + GetCharactersDrawnSize(drawFont, "A", fontHeight).y;
			float topLineY = 5.0f + (fontHeight * 1.0225f);
			DrawString(drawFont, new Vector2(20.0f, topLineY), $"{currentTime.Hour}", HorizontalAlignment.Left, -1, fontHeight);
			DrawStringOutline(drawFont, new Vector2(20.0f, topLineY), $"{currentTime.Hour}", HorizontalAlignment.Left, -1, fontHeight); //28
			float hourStrWidth = GetCharactersDrawnWidth(drawFont, $"{currentTime.Hour}", fontHeight);
			float interCharSpace = GetCharactersDrawnWidth(drawFont, "A", fontHeight);
			DrawStringOutline(drawFont, new Vector2(20.0f + hourStrWidth + interCharSpace, topLineY), $"{currentTime.Minute}", HorizontalAlignment.Left, -1, fontHeight);
		}

		public void SetMinimumSize(Vector2 newSize)
		{
			GD.Print($"FancyClock, SetMinimumSize(), new size = {newSize}");
			this.CustomMinimumSize = newSize;
			Vector2 bgSize = backgroundImage.Texture.GetSize();
			backgroundImage.Scale = new Vector2( (Size.x - 2) / bgSize.x, (Size.y - 2) / bgSize.y);
		}
	}
}
