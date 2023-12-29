using Godot;
using System;

public partial class enemy : StaticBody2D
{

	[Export] public int Health = 5;
	[Export] public string name = "enemy";
	[Export] public int Damage = 1;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
