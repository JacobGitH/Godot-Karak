using Godot;
using System;

public partial class TileMap : Godot.TileMap
{

	public override void _Ready()
	{
		//SetCell(1, new Vector2I(0, 0), 0, new Vector2I(0, 15));
	}

	public override void _Process(double _delta)
	{
	}
}
