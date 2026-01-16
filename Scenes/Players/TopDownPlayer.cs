using Godot;
using System;

public partial class TopDownPlayer : CharacterBody2D
{

	[Export]
	float speed = 100.0f;
	[Export]
	float jumpStrength = 150.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{

		MovePlayer();

		MoveAndSlide();

	}

	private void MovePlayer()
	{
		Vector2 movement = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		Velocity = movement * speed;

		if(movement.X < 0)
		{
			// Play move left animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_left");
		} 
		else if (movement.X > 0)
		{
			// Play move right animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_right");
		}
		else if (movement.Y > 0)
		{
			// Play move down animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_down");
		}
		else if (movement.Y < 0)
		{
			// Play move up animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_up");
		}
		else
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Pause();
		}
		
		
	}
}
