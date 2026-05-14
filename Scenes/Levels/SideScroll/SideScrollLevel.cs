using Godot;
using System;

public partial class SideScrollLevel : Node2D
{
	public Vector2 start;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		start = GetNode<SideScrollPlayer>("SideScrollPlayer").GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void reset_player()
	{
		SideScrollPlayer player = GetNode<SideScrollPlayer>("SideScrollPlayer");
		player.Velocity = Vector2.Zero;
		player.GlobalPosition = start;
	}

	public void _on_deathplane_body_entered(Godot.Node2D body)
	{
		reset_player();
	}

	public void _on_exit_body_entered(Godot.Node2D body)
	{
		
	}
}
