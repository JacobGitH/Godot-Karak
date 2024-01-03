using Godot;
using System;

public partial class ui : Control
{
	TextEdit TextEditDice;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TextEditDice = GetNode<TextEdit>("TextEdit");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void DisplayText(int diceRoll)
	{
		TextEditDice.Text = diceRoll.ToString();
	}
}
