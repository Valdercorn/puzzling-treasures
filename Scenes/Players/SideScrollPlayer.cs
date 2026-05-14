using Godot;
using System;

public partial class SideScrollPlayer : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;

	public Vector2 start;
	public bool doubleJumpUsed = false;

    public override void _Ready()
    {
		if(SceneManager.Instance.player_spawn_position != new Vector2(0,0))
			GlobalPosition = SceneManager.Instance.player_spawn_position;

        start = GlobalPosition;
    }


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		bool isOnFloor = IsOnFloor();
		bool isOnWall = IsOnWall();
		//FloorMaxAngle = Mathf.Pi / 2; // Use if wanting wall to become a floor
		// Add the gravity.
		if (!isOnFloor)
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || IsOnWall() || !doubleJumpUsed))
		{
			if(!isOnFloor && !isOnWall && !doubleJumpUsed)
			{
				doubleJumpUsed = true;
			}
			
			velocity.Y = JumpVelocity;

			
		}

		if (isOnFloor || isOnWall)
		{
			doubleJumpUsed = false;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			AnimatedSprite2D anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			if(velocity.X > 0.0f)
			{
				anim.FlipH = false;
			}else
			{
				anim.FlipH = true;
			}
			
			if(isOnFloor){
				anim.Play("run");
			}
		}
		else
		{
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("idle");
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if(velocity.Y > 0){
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("jump");
		} 
		else if (velocity.Y < 0){
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("fall");
		}
		

		Velocity = velocity;
		MoveAndSlide();
	}
}
