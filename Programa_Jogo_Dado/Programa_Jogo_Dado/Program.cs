using System;

class Program
{
    static void Main()
    {
        Dado dado = new Dado();
        Jogador jogador1 = new Jogador();
        Jogador jogador2 = new Jogador();

        Interfaces interfaces = new Interfaces(dado, jogador1, jogador2);
        Jogo jogo = new Jogo(dado, jogador1, jogador2);

        Aplicacao aplicacao = new Aplicacao(interfaces, jogo);

        aplicacao.Executar();
    }
}

class Pessoa
{
    protected string nome;
    protected int idade;
    protected char sexo;

    public Pessoa()
    {
        nome = "";
        sexo = '\0';
        idade = 0;
    }

    public Pessoa(string nome, int idade, char sexo)
    {
        this.nome = nome;
        this.idade = idade;
        this.sexo = sexo;
    }

    public string Nome
    {
        get => nome;
        set => nome = value;
    }

    public char Sexo
    {
        get => sexo;
        set => sexo = value;
    }

    public int Idade
    {
        get => idade;
        set => idade = value;
    }
}

class Jogador : Pessoa
{
    protected int points;

    public Jogador()
    {
        points = 0;
    }

    public Jogador(int points, string nome, int idade, char sexo) : base(nome, idade, sexo)
    {
        this.points = points;
    }

    public int Points
    {
        get => points;
        set => points = value;
    }

    public void AddPoints(int pontos)
    {
        points += pontos;
    }
}

class Dado
{
    private Random random;

    public Dado()
    {
        random = new Random();
    }

    public int JogaDado()
    {
        return random.Next(1, 7); // comando para realizar o numero aleatorio
    }
}

class Jogo
{
    private Dado dado;
    private Jogador jogador1;
    private Jogador jogador2;
    private Jogador jogadorAtual;
    private const int maxPoints = 1000; // foi definido como uma constante pois de forma alguma esse valor pode ser alterado ,  pode ser alterado apenas para testes -> mude para 1000

    public Jogo(Dado dado, Jogador jogador1, Jogador jogador2)
    {
        this.dado = dado;
        this.jogador1 = jogador1;
        this.jogador2 = jogador2;
    }

    public void IniciaJogo()
    {
        Jogador jogadorInicial = null;

        while (jogadorInicial == null)
        {
            int numeroJogador1 = dado.JogaDado();
            int numeroJogador2 = dado.JogaDado();

            Console.Clear();
            Console.WriteLine("O jogador {0} tirou o número {1} no dado.", jogador1.Nome, numeroJogador1);
            Console.WriteLine("O jogador {0} tirou o número {1} no dado.", jogador2.Nome, numeroJogador2);
            Console.ReadLine();
           

            if (numeroJogador1 > numeroJogador2)
            {
                Console.WriteLine("O jogador {0} começa o jogo pois conseguiu o maior numero!", jogador1.Nome);
                jogadorInicial = jogador1;
                Console.ReadLine();
                Console.Clear();
            }
            else if (numeroJogador2 > numeroJogador1)
            {
                Console.WriteLine("O jogador {0} começa o jogo pois conseguiu o maior numero!", jogador2.Nome);
                jogadorInicial = jogador2;
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Empate! Jogue novamente para decidir quem começa.");
                Console.ReadLine();
            }
        }

        jogadorAtual = jogadorInicial;

        while (jogador1.Points < maxPoints && jogador2.Points < maxPoints)
        {
            Console.WriteLine("Vez do jogador {0}!", jogadorAtual.Nome);

            int resultado;
            bool continuar = true;

            while (continuar)
            {
                resultado = dado.JogaDado();
                Console.WriteLine("O jogador {0} tirou {1}.", jogadorAtual.Nome, resultado);

                if (resultado == 1 || resultado == 6)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    jogadorAtual.AddPoints(resultado);
                    Console.WriteLine("O jogador {0} acumulou {1} pontos entao jogara novamente! Total: {2}", jogadorAtual.Nome, resultado, jogadorAtual.Points);
                }
                else
                {
                    jogadorAtual.AddPoints(resultado);
                    Console.WriteLine("O jogador {0} acumulou {1} pontos , passou a vez! Total: {2}. ", jogadorAtual.Nome, resultado, jogadorAtual.Points);
                    continuar = false;
                }
                Console.ResetColor();
            }

            //jogadorAtual = (jogadorAtual == jogador1) ? jogador2 : jogador1;      |      msm coisa que o if aqui embaixo esse so eh mais compacto

            if(jogadorAtual == jogador1)
            {
                jogadorAtual = jogador2;
            }
            else
            {
                jogadorAtual = jogador1;
            }
        }

        if (jogador1.Points >= maxPoints)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Parabéns jogador {0}! Você venceu com {1} pontos!", jogador1.Nome, jogador1.Points);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Jogador {0} perdeu com {1} pontos.", jogador2.Nome, jogador2.Points);
            Console.ReadLine();
        }
        else if (jogador2.Points >= maxPoints)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Parabéns jogador {0}! Você venceu com {1} pontos!", jogador2.Nome, jogador2.Points);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Jogador {0} perdeu com {1} pontos.", jogador1.Nome, jogador1.Points);
            Console.ReadLine();
        }

        Console.WriteLine("Fim do jogo!");
    }
}

class Interfaces
{
    private Jogador jogador1;
    private Jogador jogador2;
    private Dado dado;
    private Jogo jogo;

    public Interfaces(Dado dado, Jogador jogador1, Jogador jogador2)
    {
        this.dado = dado;
        this.jogador1 = jogador1;
        this.jogador2 = jogador2;
        jogo = new Jogo(dado, jogador1, jogador2);
    }

    public void PedeDados()
    {
        Console.WriteLine("Digite o nome do Jogador 1: ");
        jogador1.Nome = Console.ReadLine();
        Console.WriteLine("Digite a idade do {0}: ", jogador1.Nome);
        jogador1.Idade = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Digite o sexo do {0} (M/F): ",jogador1.Nome);
        jogador1.Sexo = Convert.ToChar(Console.ReadLine());
        Console.Clear();

        Console.WriteLine("Digite o nome do Jogador 2: ");
        jogador2.Nome = Console.ReadLine();
        Console.WriteLine("Digite a idade do {0}: ", jogador2.Nome);
        jogador2.Idade = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Digite o sexo do {0} (M/F): ", jogador2.Nome);
        jogador2.Sexo = Convert.ToChar(Console.ReadLine());
        Console.Clear();
    }

    public void ComecarJogo()
    {
        PedeDados();
        jogo.IniciaJogo();
    }
}

class Aplicacao
{
    private Interfaces aInter;
    private Jogo oJogo;

    public Aplicacao(Interfaces aInter, Jogo oJogo)
    {
        this.aInter = aInter;
        this.oJogo = oJogo;
    }

    public void Executar()
    {
        aInter.ComecarJogo();
    }
}
