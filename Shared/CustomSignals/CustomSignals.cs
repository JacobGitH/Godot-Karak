using Godot;
using System;

public partial class CustomSignals : Node
{
  [Signal] public delegate void PlaceTileEventHandler(int layer, Godot.Vector2I tile, int tileSetId, Vector2I tileCords);
}
