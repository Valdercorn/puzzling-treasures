using Godot;
using System;
using System.Runtime.InteropServices;

public partial class MovingPlatform : Node2D
{
	[Export]
	Vector2 offset = new Vector2(250, 0);
	[Export]
	float duration = 5.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Start_Tween();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Start_Tween()
	{
		AnimatableBody2D animatableBody2D = GetNode<AnimatableBody2D>("AnimatableBody2D");

		Tween tween = GetTree().CreateTween().SetProcessMode(Tween.TweenProcessMode.Physics);
		tween.SetLoops().SetParallel(false);
		tween.TweenProperty(animatableBody2D, "position", offset, duration / 2);
		tween.TweenProperty(animatableBody2D, "position", Vector2.Zero, duration / 2);
	}
}
