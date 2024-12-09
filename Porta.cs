using Godot;

public partial class Porta : Area2D
{
    [Export]
    public string nomeCenaQuiz;

    private void _on_Porta_body_entered(Node corpo)
    {
        if (corpo is Jogador)
        {
            GetTree().ChangeSceneToFile(nomeCenaQuiz);
        }
    }
	public void Abrir()
    {
        GD.Print("Entrando no quiz..." + nomeCenaQuiz);
        GetTree().ChangeSceneToFile(nomeCenaQuiz);
    }
}
