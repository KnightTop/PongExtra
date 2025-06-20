using Godot;
using System;

public partial class Player : StaticBody2D
{
	private float WindowHight;
	public static float PaddleHight;
	[Export]
	public BackGround BackGround;
	[Export]
	public Label PauseLabel;
	public override void _Ready()
	{
		PauseLabel.Hide();
		WindowHight = GetViewportRect().Size.Y;
		PaddleHight = GetNode<ColorRect>("ColorRect").Size.Y;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (BackGround.IsPause == false)
		{
			if (Input.IsActionPressed("Up"))
			{
				Position = new Godot.Vector2(Position.X, Position.Y - BackGround.PADDLESPEED * (float)delta);
			}
			else if (Input.IsActionPressed("Down"))
			{
				Position = new Godot.Vector2(Position.X, Position.Y + BackGround.PADDLESPEED * (float)delta);
			}
			Position = new Godot.Vector2(Position.X, Mathf.Clamp(Position.Y, PaddleHight / 2, WindowHight - PaddleHight / 2));
		}
		if (Input.IsActionJustPressed("Pause"))
		{
			if (BackGround.IsPause)
			{
				BackGround.IsPause = false;
				PauseLabel.Hide();
			}
			else
			{
				BackGround.IsPause = true;
				PauseLabel.Show();
			}
		}

	}
}
