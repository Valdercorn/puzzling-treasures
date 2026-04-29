using Godot;
using System;

public partial class SceneManager : Node2D
{

	public static SceneManager Instance { get; private set; }

	public Vector2 player_spawn_position {get; set;}



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
