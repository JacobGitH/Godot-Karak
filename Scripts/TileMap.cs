using Godot;
using System;

public partial class TileMap : Godot.TileMap
{
	private int layer = 1;
	Vector2I MainTileCords = new Vector2I(0, 15);
	int tileId = 0;

	public override void _Ready()
	{
		SetCell(layer, new Vector2I(0, 0), tileId, MainTileCords);
	}

	public override void _Process(double _delta)
	{
	}
}
