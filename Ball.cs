using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	private Godot.Vector2 WindowSize;
	private const float STARTSPEED = 300.0f;
	private const float ACCELERATION = 50.0f;
	private float Speed;
	private Godot.Vector2 Direction;
	private const float MAX_Y_VECTOR = 0.6f;
	[Export]
	public StaticBody2D _Player;
	[Export]
	public StaticBody2D _CPU;
	[Export]
	public BackGround BackGround;
	private float TouchTimer = 1f;

	public override void _Ready()
	{
		WindowSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (BackGround.IsPause == false)
		{
			var Collision = MoveAndCollide(Direction * Speed * (float)delta);
			if (Collision != null)
			{
				var Collider = Collision.GetCollider();
				if (Collider == _Player || Collider == _CPU)
				{
					Speed += ACCELERATION;
					Direction = NewDirection(Collision);
					TouchTimer -= (float)delta;
					if (TouchTimer <= 0 && Collider == _CPU)
					{
						BackGround.AddScoreToPlayer();
						NewBall();
					}
					else if (TouchTimer <= 0 && Collider == _Player)
					{
						BackGround.AddScoreToCPU();
						NewBall();
					}
				}
				else
				{
					Direction = Direction.Bounce(Collision.GetNormal());
				}
			}else{
				TouchTimer=1;
			}
		}

	}

	public void NewBall()
	{
		Position = new Vector2(WindowSize.X / 2, new RandomNumberGenerator().RandiRange(200, (int)WindowSize.Y - 200));
		Speed = STARTSPEED;
		Direction = RandomDirection();
	}
	private Godot.Vector2 NewDirection(KinematicCollision2D Collision)
	{
		var dist = 0f;
		if (Collision.GetCollider() == _Player)
		{
			dist = Position.Y - _Player.Position.Y;
		}
		else
		{
			dist = Position.Y - _CPU.Position.Y;
		}
		Godot.Vector2 NewDirection;
		if (Direction.X > 0)
		{
			NewDirection.X = -1;
		}
		else
		{
			NewDirection.X = 1;
		}
		NewDirection.Y = (dist / Player.PaddleHight / 2) * 5;
		return NewDirection.Normalized();
	}
	public Godot.Vector2 RandomDirection()
	{
		var Xdir = 0;
		if (new RandomNumberGenerator().Randf() > 0.5f)
		{
			Xdir = -1;
		}
		else
		{
			Xdir = 1;
		}
		return new Godot.Vector2(Xdir, new RandomNumberGenerator().RandfRange(-1, 1)).Normalized();
	}
	public void OnBallTimerTimeout()
	{
		NewBall();
	}

}
