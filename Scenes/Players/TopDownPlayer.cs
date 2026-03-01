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
			GetNode<LightOccluder2D>("LightOccluder2D").Occluder.Polygon = TurnLightOccluder("left");
		} 
		else if (movement.X > 0)
		{
			// Play move right animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_right");
			GetNode<Area2D>("InteractArea").Position = new Vector2(7, 1);
			GetNode<LightOccluder2D>("LightOccluder2D").Occluder.Polygon = TurnLightOccluder("right");
		}
		else if (movement.Y > 0)
		{
			// Play move down animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_down");
			GetNode<Area2D>("InteractArea").Position = new Vector2(0, 7);
			GetNode<LightOccluder2D>("LightOccluder2D").Occluder.Polygon = TurnLightOccluder("down");
		}
		else if (movement.Y < 0)
		{
			// Play move up animation
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move_up");
			GetNode<Area2D>("InteractArea").Position = new Vector2(0, -7);
			GetNode<LightOccluder2D>("LightOccluder2D").Occluder.Polygon = TurnLightOccluder("up");
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

	private Vector2[] TurnLightOccluder(string direction)
	{
		Vector2[] directionArray;

		if(direction == "left")
		{
			directionArray = 
				[new Vector2(1.0f, 0.0f), 
				new Vector2(1.0f, -2.0f), 
				new Vector2(-2.0f, -2.0f), 
				new Vector2(-23.0f, -14.0f), 
				new Vector2(4.0f, -2.0f), 
				new Vector2(4.0f, 2.0f), 
				new Vector2(-23.0f, 14.0f), 
				new Vector2(-2.0f, 2.0f), 
				new Vector2(1.0f, 2.0f)];
		}
		else if (direction =="right")
		{
			directionArray = 
				[new Vector2(-2.0f, 0.0f), 
				new Vector2(-2.0f, 2.0f), 
				new Vector2(2.0f, 2.0f), 
				new Vector2(23.0f, 14.0f), 
				new Vector2(-4.0f, 2.0f), 
				new Vector2(-4.0f, -2.0f), 
				new Vector2(23.0f, -14.0f), 
				new Vector2(2.0f, -2.0f), 
				new Vector2(-2.0f, -2.0f)];
		}
		else if (direction == "down")
		{
			directionArray = 
				[new Vector2(0.0f,-2.0f), 
				new Vector2(-2.0f, -2.0f), 
				new Vector2(-2.0f, 2.0f), 
				new Vector2(-14.0f, 23.0f), 
				new Vector2(-2.0f, -4.0f), 
				new Vector2(2.0f, -4.0f), 
				new Vector2(14.0f, 23.0f), 
				new Vector2(2.0f, 2.0f), 
				new Vector2(2.0f, -2.0f)];
		}
		else if (direction == "up")
		{
			directionArray = 
				[new Vector2(0.0f, 2.0f), 
				new Vector2(2.0f, 2.0f), 
				new Vector2(2.0f, -2.0f), 
				new Vector2(14.0f, -23.0f), 
				new Vector2(2.0f, 4.0f), 
				new Vector2(-2.0f, 4.0f), 
				new Vector2(-14.0f, -23.0f), 
				new Vector2(-2.0f, -2.0f), 
				new Vector2(-2.0f, 2.0f)];
		}
		else
		{
			directionArray = 
				[new Vector2(0.0f,-2.0f), 
				new Vector2(-2.0f, -2.0f), 
				new Vector2(-2.0f, 2.0f), 
				new Vector2(-14.0f, 23.0f), 
				new Vector2(-2.0f, -4.0f), 
				new Vector2(2.0f, -4.0f), 
				new Vector2(14.0f, 23.0f), 
				new Vector2(2.0f, 2.0f), 
				new Vector2(2.0f, -2.0f)];
		}

		return directionArray;
	}
}
