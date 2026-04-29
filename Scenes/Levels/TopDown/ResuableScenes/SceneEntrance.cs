using Godot;
using System;

public partial class SceneEntrance : Area2D
{

	[Export]
	string nextScene;

	[Export]
	Vector2 player_spawn_position = new Vector2();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_body_entered(Godot.Node2D body)
	{

		if(body is TopDownPlayer)
		{
			SceneManager.Instance.player_spawn_position = player_spawn_position;
			GetTree().CallDeferred("change_scene_to_file", nextScene);
		}
	}
}
