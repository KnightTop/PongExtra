using Godot;
using System;

public partial class Cpu : StaticBody2D
{
	private Godot.Vector2 BallPosition;
	private float DistanceToBall;
	private float MoveBy;
	private float WindowHeight;
	private float PaddleHeight;
	[Export]
	public BackGround BackGround;
	public override void _Ready()
	{
		WindowHeight = GetViewportRect().Size.Y;
		PaddleHeight = GetNode<ColorRect>("ColorRect").Size.Y;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (BackGround.IsPause == false)
		{
			BallPosition = GetNode<CharacterBody2D>("../Ball").Position;
			DistanceToBall = Position.Y - BallPosition.Y;
			if (Mathf.Abs(DistanceToBall) > BackGround.PADDLESPEED * delta) { MoveBy = BackGround.PADDLESPEED * (float)delta * (DistanceToBall / Mathf.Abs(DistanceToBall)); }
			else { MoveBy = DistanceToBall; }

			Position = new Godot.Vector2(Position.X, Mathf.Clamp(Position.Y - MoveBy, PaddleHeight / 2, WindowHeight - PaddleHeight / 2));
		}

	}
}
