using Godot;
using System;

public partial class TopDownPlayer : CharacterBody2D
{

	[Export]
	float speed = 100.0f;
	[Export]
	float push_strength = 100.0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{

		MovePlayer();

		PushBlocks();

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
			GetNode<Area2D>("InteractArea").Position = new Vector2(-7, 1);
		} 
		else if (movement.X > 0)
		{
			// Play move right animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_right");
			GetNode<Area2D>("InteractArea").Position = new Vector2(7, 1);
		}
		else if (movement.Y > 0)
		{
			// Play move down animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_down");
			GetNode<Area2D>("InteractArea").Position = new Vector2(0, 7);
		}
		else if (movement.Y < 0)
		{
			// Play move up animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_up");
			GetNode<Area2D>("InteractArea").Position = new Vector2(0, -7);
		}
		else
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Pause();
		}
		
		
	}

	private void PushBlocks()
	{
		// Get the last collision
		// Check if it is the block
		// if it is the block, push it
		KinematicCollision2D collision = GetLastSlideCollision();
		
		if(collision != null && IsInstanceValid(collision))
		{
			var colliderNode = collision.GetCollider() as Node;
			
			
			if(colliderNode.IsInGroup("Pushable"))
			{
				RigidBody2D colNode = colliderNode as RigidBody2D;
				Vector2 collisionNormal = collision.GetNormal();
				colNode.ApplyCentralForce(-collisionNormal * push_strength);
			}
		}
	}
}
