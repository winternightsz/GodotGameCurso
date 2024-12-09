using Godot;
using System.Collections.Generic;

public partial class Quiz : Node2D
{
    private Label perguntaLabel;
    private Button opcao1;
    private Button opcao2;
    private Button opcao3;
    private Button botaoSair;

    private int indicePerguntaAtual = 0;
    private int pontosGanhos = 0;

    private List<string> perguntas = new List<string>
    {
        "Qual é a fórmula da água?",
        "Quem descobriu o Brasil?",
        "Qual é a raiz quadrada de 9?"
    };

    private List<string[]> respostas = new List<string[]>
    {
        new string[] { "H2O", "CO2", "NaCl" },
        new string[] { "Pedro Álvares Cabral", "Cristóvão Colombo", "Dom Pedro II" },
        new string[] { "3", "9", "6" }
    };

    private List<int> respostasCorretas = new List<int> { 0, 0, 0 }; // Índices das respostas corretas

    public override void _Ready()
    {
        perguntaLabel = GetNode<Label>("PerguntaLabel");
        opcao1 = GetNode<Button>("Respostas/Opcao1");
        opcao2 = GetNode<Button>("Respostas/Opcao2");
        opcao3 = GetNode<Button>("Respostas/Opcao3");
        botaoSair = GetNode<Button>("BotaoSair");

        opcao1.Connect("pressed", new Callable(this, nameof(OnOpcao1)));
        opcao2.Connect("pressed", new Callable(this, nameof(OnOpcao2)));
        opcao3.Connect("pressed", new Callable(this, nameof(OnOpcao3)));
        botaoSair.Connect("pressed", new Callable(this, nameof(SairDoQuiz)));

        CarregarPergunta();
    }

    private void CarregarPergunta()
    {
        if (indicePerguntaAtual < perguntas.Count)
        {
            perguntaLabel.Text = perguntas[indicePerguntaAtual];
            opcao1.Text = respostas[indicePerguntaAtual][0];
            opcao2.Text = respostas[indicePerguntaAtual][1];
            opcao3.Text = respostas[indicePerguntaAtual][2];
        }
        else
        {
            FinalizarQuiz();
        }
    }

    private void VerificarResposta(int opcaoEscolhida)
    {
        if (opcaoEscolhida == respostasCorretas[indicePerguntaAtual])
        {
            pontosGanhos++;
        }

        indicePerguntaAtual++;
        CarregarPergunta();
    }

    private void FinalizarQuiz()
{
    GD.Print($"Quiz concluído! Pontos ganhos: {pontosGanhos}");

    // Adiciona os pontos ao jogo principal
    Jogo jogo = (Jogo)GetNode("/root/Jogo");
    jogo.AdicionarPontuacao(pontosGanhos);

	HUD hud = GetNode<HUD>("/root/HUD");
    hud.AtualizarHUD(jogo.diaAtual, jogo.ObterMateriaDoDia(), jogo.pontuacaoTotal);

    // Retorna ao cenário principal
    GetTree().ChangeSceneToFile("res://CenarioPrincipal.tscn");
}


    private void OnOpcao1()
    {
        VerificarResposta(0);
    }

    private void OnOpcao2()
    {
        VerificarResposta(1);
    }

    private void OnOpcao3()
    {
        VerificarResposta(2);
    }

    private void SairDoQuiz()
    {
        GD.Print("Saindo do quiz.");
        GetTree().ChangeSceneToFile("res://CenarioPrincipal.tscn");
    }
}
