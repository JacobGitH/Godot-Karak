using Godot;
using System;

public partial class Enemy : StaticBody2D
{

	[Export] public int Health = 5;
	[Export] public string EnemyName = "enemy";
	[Export] public int Damage = 1;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void OnBody2DEntered(Player body)
	{
		if (body.HasMethod("StartCombat"))
		{
			bool result = body.StartCombat(Health);
			if (result == true)
			{
				this.QueueFree();
			}
		}
	}
}
