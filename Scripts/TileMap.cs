using Godot;
using System;

public partial class TileMap : Godot.TileMap
{
	private int layer = 1;
	Vector2I MainTileCords = new Vector2I(0, 15);
	int tileId = 0;

	//Signals
	private CustomSignals customSignals;

	public override void _Ready()
	{
		SetCell(layer, new Vector2I(0, 0), tileId, MainTileCords);
		customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		customSignals.PlaceTile += PlaceTile;
	}

	public override void _Process(double _delta)
	{
	}

	private void PlaceTile(int layer, Godot.Vector2I tile, int tileSetId, Vector2I tileCords)
	{

		SetCell(layer, tile, tileSetId, tileCords);
	}


}
