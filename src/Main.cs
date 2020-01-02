// Copyright (c) 2017 Leacme (http://leac.me). View LICENSE.md for more information.
using Godot;
using System;
using System.Linq;

public class Main : Spatial {

	public AudioStreamPlayer Audio { get; } = new AudioStreamPlayer();

	private PackedScene ballScene = GD.Load<PackedScene>("res://scenes/Ball.tscn");

	private void InitSound() {
		if (!Lib.Node.SoundEnabled) {
			AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), true);
		}
	}

	public override void _Notification(int what) {
		if (what is MainLoop.NotificationWmGoBackRequest) {
			GetTree().ChangeScene("res://scenes/Menu.tscn");
		}
	}

	public override void _Ready() {
		GetNode<WorldEnvironment>("sky").Environment.BackgroundColor = new Color(Lib.Node.BackgroundColorHtmlCode);
		InitSound();
		AddChild(Audio);

		GetNode("Board/BoardPhysics").GetChildren().Cast<Node>().ToList().ForEach(z => z.GetChild<MeshInstance>(0).MaterialOverride = new SpatialMaterial() {
			AlbedoColor = new Color(1, 1, 1, 0.3f),
			NormalEnabled = true,
			NormalScale = -0.1f,
			RefractionEnabled = true,
			RefractionScale = 0.1f,
		});

		SpawnBall();
	}

	private void SpawnBall() {
		var ball = (Spatial)ballScene.Instance();
		ball.GetNode<MeshInstance>("BallPhysics/BallCollision/Ball").MaterialOverride = new SpatialMaterial() {
			AlbedoColor = Color.FromHsv(GD.Randf(), 1, 1)
		};
		ball.Translate(new Vector3(-0.2f, 2.8f, 0));
		AddChild(ball);
	}

	public override void _Input(InputEvent @event) {
		if (@event is InputEventScreenTouch st && st.Pressed) {
			SpawnBall();
		}
	}

	public override void _Process(float delta) {

	}

}
