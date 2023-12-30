using Godot;
using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{

	const int tileSize = 32;
	const int layer = 1;
	const int hoverLayer = 2;
	const string isWalkable = "isWalkable";
	int y;
	int x = 0;
	int tileSetId = 0;
	int tileHoverId = 1;
	string RoadDown = "RoadDown";
	string RoadUp = "RoadUp";
	string RoadLeft = "RoadLeft";
	string RoadRight = "RoadRight";
	Random rand = new Random();
	Vector2I tileRemember;

	//Godot Classes
	TileData tileData;

	//Nodes
	private static TileMap tileMap;

	public override void _Ready()
	{
		//rdy for mult full setup
		// GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").SetMultiplayerAuthority(int.Parse(this.Name));
		// if (GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").GetMultiplayerAuthority() == Multiplayer.GetUniqueId())
		// {
		tileMap = GetNode<TileMap>("../TileMap");
		y = rand.Next(0, 10);
		//}

	}

	public override void _PhysicsProcess(double _delta)
	{
		// if (GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").GetMultiplayerAuthority() == Multiplayer.GetUniqueId())
		// {
		Movement();
		TilePlacement();
		//}
	}

	#region PlayerMovement
	private void Movement()
	{
		PlayerMovement();
	}

	//GOD HAVE MERCY
	private void SideMove(int X, int Y, string playerPositionRoad, string nextPositionRoad)
	{
		Godot.Vector2 cords = new Godot.Vector2(X, Y);
		Vector2I mapCords = tileMap.LocalToMap(new Godot.Vector2(X, Y));
		Vector2I playerCords = tileMap.LocalToMap(this.Position);
		Vector2I nextCords = playerCords + mapCords;

		tileData = tileMap.GetCellTileData(layer, nextCords);

		bool nextTile = false;
		if (tileData != null)
		{
			nextTile = (bool)tileData.GetCustomData(nextPositionRoad);
		}

		tileData = tileMap.GetCellTileData(layer, playerCords);
		bool playerTile = (bool)tileData.GetCustomData(playerPositionRoad);

		if (playerTile && nextTile)
		{
			this.Position += cords;
		}
	}

	private void PlayerMovement()
	{
		if (Input.IsActionJustPressed("ui_left"))
		{
			SideMove(-tileSize, 0, RoadLeft, RoadRight);
		}
		else if (Input.IsActionJustPressed("ui_right"))
		{
			SideMove(tileSize, 0, RoadRight, RoadLeft);
		}
		else if (Input.IsActionJustPressed("ui_up"))
		{
			SideMove(0, -tileSize, RoadUp, RoadDown);
		}
		else if (Input.IsActionJustPressed("ui_down"))
		{
			SideMove(0, tileSize, RoadDown, RoadUp);
		}
	}
	#endregion

	#region TilePlacement
	private void TilePlacement()
	{
		PlaceTile();
		RotateTile();
		showPlayerTileHover();
	}

	public void PlaceTile()
	{
		if (Input.IsActionJustPressed("mb_left"))
		{
			var tile = tileMap.LocalToMap(GetGlobalMousePosition());
			Vector2I playerPosition = tileMap.LocalToMap(this.Position);
			if ((tile.X == playerPosition.X + 1 && tile.Y == playerPosition.Y)
			|| (tile.X == playerPosition.X - 1 && tile.Y == playerPosition.Y)
			|| (tile.Y == playerPosition.Y + 1 && tile.X == playerPosition.X)
			|| (tile.Y == playerPosition.Y - 1 && tile.X == playerPosition.X))
			{
				if (tileMap.GetCellSourceId(layer, tile) != tileSetId)
				{
					tileMap.SetCell(layer, tile, tileSetId, new Vector2I(x, y));
					y = rand.Next(0, 10);
				}
			}
		}
	}

	public void RotateTile()
	{
		if (Input.IsActionJustPressed("rotate_tile"))
		{
			x = (x < 3) ? x + 1 : 0;
		}
	}

	public void showPlayerTileHover()
	{
		var tile = tileMap.LocalToMap(GetGlobalMousePosition());
		if (tileMap.GetCellSourceId(layer, tile) != tileSetId)
		{
			if (tileRemember != tile)
			{
				tileMap.EraseCell(hoverLayer, tileRemember);
				tileRemember = tile;
			}
			tileMap.SetCell(hoverLayer, tile, tileHoverId, new Vector2I(x, y));
		}
	}
	#endregion

}
