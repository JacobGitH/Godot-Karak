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
		//Custom Signals
		customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		customSignals.PlaceTile += PlaceTile;

		//starting cell
		SetCell(layer, new Vector2I(0, 0), tileId, MainTileCords);
	}

	public override void _Process(double _delta)
	{
	}

	private void PlaceTile(int layer, Godot.Vector2I tile, int tileSetId, Vector2I tileCords)
	{
		SetCell(layer, tile, tileSetId, tileCords);
		CheckTileForEnemySpawn(tile, tileCords);
	}

	private void CheckTileForEnemySpawn(Vector2I tile, Vector2I tileCords)
	{
		if (tileCords.Y > 4 && tileCords.Y < 10)
		{
			SpawnEnemyOnTile(tile);
		}
	}

	private void SpawnEnemyOnTile(Vector2I tile)
	{
		var enemy = GD.Load<PackedScene>("res://Enemy/enemy.tscn");
		var enemyInst = enemy.Instantiate<Node2D>();

		enemyInst.Position = MapToLocal(tile);
		AddChild(enemyInst);
	}

}
