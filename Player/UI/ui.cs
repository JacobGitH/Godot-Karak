using Godot;
using System;

public partial class ui : Control
{
	TextEdit TextEditDice;
	TextEdit Moves;
	TextEdit Health;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TextEditDice = GetNode<TextEdit>("TextEdit");
		Moves = GetNode<TextEdit>("Moves");
		Health = GetNode<TextEdit>("Health");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void DisplayMoves(int moves)
	{
		Moves.Text = moves.ToString();
	}

	public void DisplayText(int diceRoll)
	{
		TextEditDice.Text = diceRoll.ToString();
	}

	public void DisplayHealth(int health)
	{
		Health.Text = health.ToString();
	}


}
