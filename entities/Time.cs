using TXTSoccer.helpers;

namespace TXTSoccer.entities
{
    internal class Time
    {
        public string Nome { get; }
        internal List<Jogador> Plantel { get; set; } = new();

        public Time(string nome)
        {
            Nome = nome;
        }

        /// <summary>
        /// Faz a escalação do time.<br/>
        /// Usa o <see cref="Plantel"/> do time devolve uma lista com 11 titulares e 7 reservas.
        /// </summary>
        /// <returns>Lista de <see cref="Jogador"> titulares e reservas.</returns>
        public List<Jogador> EscalarJogadores()
        {
            Dictionary<Posicao, int> formacao = HelperDataHolder.Instance.formacao;
            List<Jogador> disponivel = new(Plantel.FindAll(j => !j.Suspenso));
            List<Jogador> selecionados = new();

            // Seleciona os titulares de acordo com a quantidade de jogadores em cada posição
            // Compara os jogadores com posições iguais (ordem decrescente).
            formacao.Keys.ToList().ForEach(p =>
            {
                int counter = 0;
                List<Jogador> jogadoresDaPosicao = disponivel.FindAll(j => j.Posicao == p);
                jogadoresDaPosicao.Sort((x, y) => y.Qualidade.CompareTo(x.Qualidade));

                for (int i = 0; counter < formacao.GetValueOrDefault(p) && i < jogadoresDaPosicao.Count; i++)
                {
                    selecionados.Add(jogadoresDaPosicao[i]);
                    disponivel.Remove(jogadoresDaPosicao[i]);

                    counter++;
                }
            });

            // Caso tenha menos jogadores que o necessário para as posições, os lugares são preenchidos
            // com os melhores jogadores, não importando a posição em que joguem
            disponivel.Sort((x, y) => y.Qualidade.CompareTo(x.Qualidade));

            int jogadoresFaltantes = 11 - selecionados.Count;

            if (jogadoresFaltantes > 0)
            {
                selecionados.AddRange(disponivel.GetRange(0, jogadoresFaltantes - 1));
                disponivel.RemoveRange(0, jogadoresFaltantes - 1);
            }

            // Preencher as vagas dos reservas
            selecionados.AddRange(disponivel.Count >= 7 ? disponivel.GetRange(0, 7) : disponivel);

            return selecionados;
        }
    }
}
