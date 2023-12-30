using Godot;
using System;
using System.Reflection;

public partial class multiplayer_menu : Control
{

	[Export] private int port = 8911;
	[Export] private string address = "127.0.0.1";
	[Export] private int numberOfPlayers = 2;

	private ENetMultiplayerPeer peer;


	public override void _Ready()
	{
		Multiplayer.PeerConnected += PeerConnected;
		Multiplayer.PeerDisconnected += PeerDisconnected;
		Multiplayer.ConnectedToServer += ConnectedToServer;
		Multiplayer.ConnectionFailed += ConnectionFailed;
	}

	// runs when the connection fails and it runs only on client
	private void ConnectionFailed()
	{
		GD.Print("Connection Failed");
	}

	//runs on client
	private void ConnectedToServer()
	{
		GD.Print("Connected to server");
		RpcId(1, "SendPlayerInformation", GetNode<LineEdit>("LineEdit").Text, Multiplayer.GetUniqueId());
	}

	//runs on all peers
	private void PeerDisconnected(long id)
	{
		GD.Print("PlayerDisconnected" + id.ToString());
	}

	//runs on all peers
	private void PeerConnected(long id)
	{
		GD.Print("PlayerConnected" + id.ToString());
	}

	public void OnHostButtonDown()
	{
		peer = new ENetMultiplayerPeer();
		var error = peer.CreateServer(port, numberOfPlayers);
		if (error != Error.Ok)
		{
			GD.Print("Error: Cannot Host:" + error.ToString());
			return;
		}
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Waiting For Players");
		SendPlayerInformation(GetNode<LineEdit>("LineEdit").Text, 1);
	}

	public void OnJoinButtonDown()
	{
		peer = new ENetMultiplayerPeer();
		peer.CreateClient(address, port);
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Player Joining");
	}

	public void OnStartButtonDown()
	{
		Rpc("StartGame");
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	private void StartGame()
	{
		foreach (var item in GameManager.Players)
		{
			GD.Print(item.Name + "is Playing");
		}
		var scene = ResourceLoader.Load<PackedScene>("res://World/world.tscn").Instantiate<Node2D>();
		GetTree().Root.AddChild(scene);
		this.Hide();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void SendPlayerInformation(string name, int id)
	{
		PlayerInfo playerInfo = new PlayerInfo()
		{
			Name = name,
			Id = id
		};

		if (!GameManager.Players.Contains(playerInfo))
		{
			GameManager.Players.Add(playerInfo);
		}

		if (Multiplayer.IsServer())
		{
			foreach (var item in GameManager.Players)
			{
				Rpc("SendPlayerInformation", item.Name, item.Id);
			}
		}

	}

}
