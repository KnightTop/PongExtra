using Godot;
using System;

public partial class BackGround : Sprite2D
{
	public Score score = new Score(0, 0);
	public static float PADDLESPEED = 500;
	public Ball Ball = new Ball();
	public bool IsPause=false;
	public override void _Ready()
	{
	}


	public override void _Process(double delta)
	{
	}
	public void OnBallTimerTimeout()
	{
	}
	public void OnScoreCPUBodyEntered(PhysicsBody2D body)
	{
		AddScoreToCPU();
		GetNode<Timer>("BallTimer").Start();
	}
	public void OnScorePlayerBodyEntered(PhysicsBody2D body)
	{
		AddScoreToPlayer();
		GetNode<Timer>("BallTimer").Start();
	}
	public void AddScoreToPlayer()
	{
		score._PlayerScore++;
		GetNode<CanvasLayer>("HUD").GetNode<Label>("PlayerScore").Text = score._PlayerScore.ToString();
	}
	public void AddScoreToCPU()
	{
		score._CPUScore++;
		GetNode<CanvasLayer>("HUD").GetNode<Label>("CPUScore").Text = score._CPUScore.ToString();
	}
}
public class Score
{
	public int _PlayerScore;
	public int _CPUScore;
	public Score(int PlayerScore, int CPUScore)
	{
		_PlayerScore = PlayerScore;
		_CPUScore = CPUScore;
	}
}
