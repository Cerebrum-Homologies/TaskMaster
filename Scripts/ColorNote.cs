using Godot;
using System;
using TaskMaster.Framework;

namespace TaskMaster
{
	public partial class ColorNote : Control
	{
		private MarginContainer marginContainer;
		private HFlowContainer flowLayoutContainer;
		private ColorRect colorRect;
		private Sprite2D spriteImage;

		public override void _Ready()
		{
			GD.Print("ColorNote, _Ready() called");
			marginContainer = GetNodeOrNull<MarginContainer>("MarginContainer");
			colorRect = GetNodeOrNull<ColorRect>("ColorRect");
			spriteImage = colorRect.GetNodeOrNull<Sprite2D>("Sprite2D");
			Diagnostics.PrintNullValueMessage(marginContainer, "marginContainer");
			if (marginContainer != null)
			{
				marginContainer.Size = Size;
				colorRect.Size = Size;
				//float scaleBackTextureX = (Size.x / spriteImage.Texture.GetSize().x) + 0.01f;
				//float scaleBackTextureY = (Size.y / spriteImage.Texture.GetSize().y) + 0.01f;
				float scaleBackTextureX = 2.0f;
				float scaleBackTextureY = 2.0f;
				spriteImage.Scale = new Vector2(scaleBackTextureX, scaleBackTextureY);
				flowLayoutContainer = marginContainer.GetNodeOrNull<HFlowContainer>("HFlowContainer");
				Diagnostics.PrintNullValueMessage(flowLayoutContainer, "flowLayoutContainer");
				flowLayoutContainer.CustomMinimumSize = Size;
				flowLayoutContainer.Size = Size;
			}
			Diagnostics.PrintObjectProperties("ColorNote", this);
		}

		public void SetMinimumSize(Vector2 newSize)
		{
			this.CustomMinimumSize = newSize;
			marginContainer.Size = newSize;
			colorRect.Size = newSize;
			//float scaleBackTextureX = (Size.x / spriteImage.Texture.GetSize().x) + 0.01f;
			//float scaleBackTextureY = (Size.y / spriteImage.Texture.GetSize().y) + 0.01f;
			//spriteImage.Scale = new Vector2(scaleBackTextureX, scaleBackTextureY);
		}

		public void ExampleNoteGenerate()
		{
			if (flowLayoutContainer != null)
			{
				Label txtLabel = new Label();
				txtLabel.Name = "LX1";
				txtLabel.AutowrapMode = TextServer.AutowrapMode.Word;
				txtLabel.Text = "Happy froopings and salicusness";
				txtLabel.Modulate = new Color(0.75f, 0.275f, 0.15f, 0.925f);
				txtLabel.Visible = true;
				txtLabel.CustomMinimumSize = new Vector2(180, 40);
				txtLabel.Size = new Vector2(this.Size.x - 20.0f, 40.0f);
				//txtLabel.AddColorOverride("font_color", new Color(1.0f, 0.625f, 0.75f, 1.0f));
				txtLabel.BeginBulkThemeOverride();
				txtLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.625f, 0.75f, 1.0f));
				txtLabel.AddThemeColorOverride("font_shadow_color", new Color(0.125f, 0.425f, 0.175f, 0.775f));
				txtLabel.AddThemeFontSizeOverride("font_size", 20);
				txtLabel.AddThemeConstantOverride("shadow_offset_x", 3);
				txtLabel.EndBulkThemeOverride();
				Label txtLabel2 = (Label)txtLabel.Duplicate();
				txtLabel2.Name = "LX2";
				txtLabel2.Text = "A new day and dawn have begun !!!";
				txtLabel2.AddThemeColorOverride("font_color", new Color(0.65f, 0.625f, 0.925f, 1.0f));
				txtLabel2.AddThemeFontSizeOverride("font", 18);
				//txtLabel2.Position = new Vector2(10.0f, 65.0f);
				txtLabel2.Visible = true;
				Label txtLabel3 = (Label)txtLabel.Duplicate();
				txtLabel3.Name = "LX3";
				txtLabel3.Text = "Many manu and manifold meanderings magnificently";
				txtLabel3.AddThemeColorOverride("font_color", new Color(0.95f, 0.975f, 1.0f, 1.0f));
				txtLabel3.AddThemeFontSizeOverride("font", 16);
				txtLabel3.Visible = true;
				flowLayoutContainer.AddChild(txtLabel);
				flowLayoutContainer.AddChild(txtLabel2);
				flowLayoutContainer.AddChild(txtLabel3);
				GD.Print($"ColorNote, ExampleNoteGenerate(), label = {txtLabel}");
				Diagnostics.PrintChildrenInfo("flowLayoutContainer", flowLayoutContainer);
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}
