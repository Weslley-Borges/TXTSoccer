using TXTSoccer.entities;

namespace TXTSoccer.helpers
{
    internal class Posicao
    {
        public string Nome { get; }
        public string Sigla { get; }

        public Posicao(string nome, string sigla)
        {
            Nome = nome;
            Sigla = sigla;
        }
    }

    internal sealed class HelperDataHolder
    {
        public static HelperDataHolder? instance = null;
        public List<Time> times = new() 
        {
            new("Real Marola"),
            new ("Los Cucos"),
            new ("Xilindro"),
            new ("Zaparov"),
            new ("Atletico MG"),
            new ("Gremio"),
            new ("Bahia"),
            new ("Vitoria"),
            new ("Panthers"),
            new ("Supercampeoes"),
            new ("Los Revisionistas"),
            new ("Tropa do Calvo"),
            new ("Os Perna-Bamba"),
            new ("Cabeca pesada")
        };

        public Dictionary<Posicao, int> formacao = new()
        {
            {new Posicao("Atacante", "AT"), 3 },
            {new Posicao("Meio-campo", "MC"), 2 },
            {new Posicao("Zagueiro", "ZG"), 3 },
            {new Posicao("Goleiro", "GL"), 1 },
            {new Posicao("Lateral", "LA"), 2 }
        };

        public List<string> nomesJogadores = new()
        {
            "Vinicius", "Bryan", "Aarao", "Kelvin", "Anderson",
            "Pietro", "Caua", "Diego", "Duarte", "Juliano", "Murilo",
            "Vicente", "Vinicios", "Angelo", "Douglas", "Joao",
            "Miguel", "Vicente", "Henrique", "Santiago", "Raul",
            "Leonardo", "Pietro", "Guilherme","Leonardo","Vinicios"
        };

        public List<string> apelidosJogadores = new()
        {
            "Alface", "Perna de Ouro", "Bambi", "Vara", "Mestre",
            "SIIIIU", "Camarão", "Maré", "Pequeno"
        };

        public static HelperDataHolder Instance
        {
            get
            {
                instance ??= new HelperDataHolder();
                return instance;
            }
        }

    }
}
