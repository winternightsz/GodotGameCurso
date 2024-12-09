using Godot;

public partial class Jogador : CharacterBody2D
{
	[Export]
	public int velocidade = 200;

	private RayCast2D rayCastInteracao;
	private Camera2D camera;

	public override void _Ready()
	{
		rayCastInteracao = GetNode<RayCast2D>("RayCastInteracao");
		camera = GetNode<Camera2D>("Camera2D");
	}

	public void _PhysicsProcess(float delta)
	{
		Vector2 direcao = Vector2.Zero;

		if (Input.IsActionPressed("ui_up"))
			direcao.Y -= 1;
		if (Input.IsActionPressed("ui_down"))
			direcao.Y += 1;
		if (Input.IsActionPressed("ui_left"))
			direcao.X -= 1;
		if (Input.IsActionPressed("ui_right"))
			direcao.X += 1;

		Velocity = direcao.Normalized() * velocidade;
		MoveAndSlide();

		VerificarInteracao();
	}

	private void VerificarInteracao()
	{
		if (Input.IsActionJustPressed("ui_accept") && rayCastInteracao.IsColliding())
		{
			GodotObject objeto = rayCastInteracao.GetCollider();
			if (objeto is NPC npc)
			{
				npc.Interagir();
			}
			else if (objeto is Item item)
			{
				item.Coletar();
			}
			else if (objeto is Porta porta)
            {
                porta.Abrir();
            }
		}
	}
}
