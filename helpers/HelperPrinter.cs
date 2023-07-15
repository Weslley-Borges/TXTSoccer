using TXTSoccer.entities;
using System.Text;

namespace TXTSoccer.helpers
{
    /// <summary>
    /// Uma classe para controlar a forma como as informações serão mostradas no terminal
    /// </summary>
    internal class HelperPrinter
    {
        public static HelperPrinter? instance = null;

        public static HelperPrinter Instance
        {
            get
            {
                instance ??= new HelperPrinter();
                return instance;
            }
        }

        /// <summary>
        /// Mostra uma mensagem enquanto espera o usuário apertar a tecla ENTER
        /// </summary>
        /// <param name="message">A mensagem mostrada no terminal</param>
        public static void WaitKey(string message)
        {
            Console.WriteLine($"\n\n[!] APERTE ENTER PARA {message}");

            while (true)
            {
                var ch = Console.ReadKey(false).Key;

                if (ch == ConsoleKey.Enter)
                {
                    Console.Clear();
                    return;
                }
            }
        }

        /// <summary>
        /// Imprime uma tabela dos <see cref="Jogador">jogadores</see> de um <see cref="Time"/>.
        /// </summary>
        /// <param name="t">O <see cref="Time"/> do qual serão usados os jogadores</param>
        public static void ImprimirJogadores(Time t)
        {
            Console.WriteLine($"\n\nJOGADORES REGISTRADOS - {t.Nome}");

            Table tbl = new("Nome", "Apelido", "Numero", "Posicao", "Data de nascimento", "Condicao", "Qualidade");

            t.Plantel.ForEach(j =>
            {
                string status = j.VerificarCondicaoDeJogo() == true ? "SUSPENSO" : "TA PRA JOGO";

                tbl.AddRow(
                    j.Nome, j.Apelido, j.Numero, j.Posicao.Nome,
                    $"{j.DataNascimento.Day}/{j.DataNascimento.Month}/{j.DataNascimento.Year}",
                    status, j.Qualidade
                    );
            });

            tbl.Print();
        }

        public static void ImprimirJogadores(string nomeTime, List<Jogador> escalacao)
        {
            Console.WriteLine($"\n\nESCALACAO - {nomeTime}");

            Table tbl = new("Nome", "Apelido", "Numero", "Posicao", "Qualidade", "Status");

            escalacao.ForEach(j =>
            {
                string reserva = escalacao.IndexOf(j) < 11 ? "TITULAR" : "RESERVA";
                tbl.AddRow(j.Nome, j.Apelido, j.Numero, j.Posicao.Nome, j.Qualidade, reserva);

            });

            tbl.Print();
        }

        public static void ImprimirTabelaTimesCampeonato(Campeonato c)
        {
            Console.WriteLine($"{c.Name} - Rodada {c.RodadaAtual}/{c.Rodadas}");

            c.Times.Sort((x, y) => y.GetPontuacao().CompareTo(x.GetPontuacao()));

            Table tbl = new("Posicao", "Nome do Time", "Derrotas", "Empates", "Vitorias", "Pontos");

            for (int i=0; i < c.Times.Count; i++)
            {
                TimeParticipante t = c.Times[i];
                tbl.AddRow(i + 1, t.Time.Nome, t.derrotas, t.empates, t.vitorias, t.GetPontuacao());
            }

            tbl.Print();

            if (c.RodadaAtual == c.Rodadas)
            {
                Console.WriteLine($"\n\n[!] O time {c.Times[0].Time.Nome} venceu o campeonato!");
                WaitKey("FINALIZAR O PROGRAMA");
            }
            else WaitKey("COMECAR A PROXIMA RODADA");
        }
    }

    // Créditos: https://genert.org/blog/csharp-programming/
    // Fiz algumas alterações, para a tabela ficar mais minimalista
    internal class Table
    {
        private List<object> _Columns { get; set; }
        private List<object[]> _Rows { get; set; }

        public Table(params string[] columns)
        {

            if (columns == null || columns.Length == 0)
                throw new ArgumentException("Parameter cannot be null nor empty", "columns");

            _Columns = new List<object>(columns);
            _Rows = new List<object[]>();
        }

        public void AddRow(params object[] values)
        {
            if (values == null)
                throw new ArgumentException("Parameter cannot be null", "values");

            if (values.Length != _Columns.Count)
                throw new Exception("The number of values in row does not match columns count.");

            _Rows.Add(values);
        }

        public override string ToString()
        {
            StringBuilder tableString = new StringBuilder();
            List<int> columnsLength = GetColumnsMaximumStringLengths();

            var rowStringFormat = Enumerable
                .Range(0, _Columns.Count)
                .Select(i => " | {" + i + ",-" + columnsLength[i] + "}")
                .Aggregate((total, nextValue) => total + nextValue) + " |";

            string columnHeaders = string.Format(rowStringFormat, _Columns.ToArray());
            List<string> results = _Rows.Select(row => string.Format(rowStringFormat, row)).ToList();

            int maximumRowLength = Math.Max(0, _Rows.Any() ? _Rows.Max(row => string.Format(rowStringFormat, row).Length) : 0);
            int maximumLineLength = Math.Max(maximumRowLength, columnHeaders.Length);

            string dividerLine = string.Join("", Enumerable.Repeat("—", maximumLineLength - 1));
            string divider = $" {dividerLine} ";

            tableString.AppendLine(divider);
            tableString.AppendLine(columnHeaders);
            tableString.AppendLine(divider);


            foreach (var row in results)
                tableString.AppendLine(row);

            tableString.AppendLine(divider);

            return tableString.ToString();
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }


        private List<int> GetColumnsMaximumStringLengths()
        {
            List<int> columnsLength = new List<int>();

            for (int i = 0; i < _Columns.Count; i++)
            {
                List<object> columnRow = new List<object>();
                int max = 0;

                columnRow.Add(_Columns[i]);

                for (int j = 0; j < _Rows.Count; j++)
                {
                    columnRow.Add(_Rows[j][i]);
                }

                for (int n = 0; n < columnRow.Count; n++)
                {
                    int len = columnRow[n].ToString().Length;

                    if (len > max)
                    {
                        max = len;
                    }
                }

                columnsLength.Add(max);
            }

            return columnsLength;
        }
    }
}
