using Godot;
using System;
using TaskMaster.Framework;

namespace TaskMaster
{
	public partial class WorkDesk : Node2D
	{
		private SceneUtilities sceneUtilities;
		private GridContainer panelContainerWorkdesk;
		private PanelContainer panelContainerWorkArea;
		private RichTextLabel labelTitle;
		private ColorRect colorRect;
		private ColorNote colorNote;
		private Tween panelWorkAreaZapper = null;
		
		public void WorkAreaFizzer()
		{
			labelTitle.Modulate = new Color(0.95f, 0.85f, 0.925f, 1.0f);
			panelContainerWorkArea.Visible = false;
			panelContainerWorkArea.Modulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			panelContainerWorkdesk.Visible = false;
			panelContainerWorkdesk.Visible = true;
			GD.Print($"WorkDesk, WorkAreaFizzer(), panelContainer.Visible = {panelContainerWorkdesk.Visible}");
			if (panelWorkAreaZapper != null) {
				Callable panelWorkChangeCall = new Callable(this, nameof(FixPanelLayout));
				panelWorkChangeCall.Call(true);
				Tween quickTweenerLocal = this.CreateTween();
				quickTweenerLocal.BindNode(panelContainerWorkArea);
				quickTweenerLocal.TweenCallback(panelWorkChangeCall).SetDelay(3.0f);
			}
		}

		public void FixPanelLayout(bool visibility)
		{
			GD.Print($"WorkDesk, FixPanelLayout(), visibility = {visibility}");
			labelTitle.SizeFlagsVertical = (int)Control.SizeFlags.ShrinkBegin;
			panelContainerWorkArea.SizeFlagsVertical = /*(int)*/Control.SizeFlags.ShrinkEnd;
			panelContainerWorkdesk.Visible = false;
			labelTitle.Visible = false;
			panelContainerWorkArea.Visible = false;
			panelContainerWorkdesk.Visible = visibility;
			labelTitle.Visible = visibility;
			panelContainerWorkArea.Visible = visibility;
		}

		public override void _Ready()
		{
			Rect2 appWindowRect = SceneUtilities.GetApplicationWindowExtent(this);
			panelContainerWorkdesk = GetNodeOrNull<GridContainer>("Container-WorkDesk");
			Diagnostics.PrintNullValueMessage(panelContainerWorkdesk, "panelContainerWorkdesk");
			GD.Print($"WorkDesk, _Ready() called, panelContainer = {panelContainerWorkdesk}");
			if (panelContainerWorkdesk != null)
			{
				panelContainerWorkdesk.Size = appWindowRect.Size;
				labelTitle = panelContainerWorkdesk.GetNodeOrNull<RichTextLabel>("RTLabel-Title");
				labelTitle.Modulate = new Color(0.45f, 0.525f, 0.925f, 0.075f);
				labelTitle.SizeFlagsVertical = (int)Control.SizeFlags.ShrinkBegin;
				panelContainerWorkArea = panelContainerWorkdesk.GetNodeOrNull<PanelContainer>("PanelContainer-Space");
				Diagnostics.PrintNullValueMessage(panelContainerWorkArea, "panelContainerWorkArea");
				panelContainerWorkArea.Position = new Vector2(0, labelTitle.Size.y - 1);
				panelContainerWorkArea.Modulate = new Color(0.35f, 0.725f, 0.15f, 0.125f);
				panelContainerWorkArea.CustomMinimumSize = new Vector2(appWindowRect.Size.x, appWindowRect.Size.y - labelTitle.Size.x);
				panelContainerWorkArea.SizeFlagsVertical = /*(int)*/Control.SizeFlags.ExpandFill;
				//panelContainerWorkArea.Visible = false;
				panelWorkAreaZapper = GetTree().CreateTween();
				panelWorkAreaZapper.BindNode(this);
				//panelWorkAreaZapper.TweenProperty(panelContainerWorkArea, "modulate", new Color(0.35f, 0.725f, 0.15f, 0.875f), 3.0f);
				//panelWorkAreaZapper.TweenProperty(panelContainerWorkArea, "modulate", new Color(0.15f, 0.125f, 0.75f, 0.075f), 9.0f);
				panelWorkAreaZapper.TweenProperty(panelContainerWorkArea, "modulate", new Color(0.35f, 0.725f, 0.15f, 0.875f), 1.5f);
				panelWorkAreaZapper.TweenProperty(panelContainerWorkArea, "modulate", new Color(0.15f, 0.125f, 0.75f, 0.075f), 1.5f);
				panelWorkAreaZapper.TweenCallback(new Callable(this, nameof(WorkAreaFizzer))).SetDelay(1.0f);
				colorRect = GetNodeOrNull<ColorRect>("ColorRect");
				Diagnostics.PrintNullValueMessage(colorRect, "colorRect");
				colorRect.Position = new Vector2(0, labelTitle.Size.y - 1);
				colorRect.Size = new Vector2(appWindowRect.Size.x, appWindowRect.Size.y - labelTitle.Size.x);
				colorNote = panelContainerWorkArea.GetNodeOrNull<ColorNote>("ColorNote");
				Diagnostics.PrintNullValueMessage(colorNote, "colorNote");
				colorNote.SetMinimumSize(new Vector2(600.0f, 420.0f));
				if (colorNote != null)
				{
					colorNote.ExampleNoteGenerate();
				}            
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}
