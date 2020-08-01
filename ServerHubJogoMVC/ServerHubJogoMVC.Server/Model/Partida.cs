using ServerHubJogoMVC.Server.Model.Result;
using System.Collections.Generic;
using System.Linq;

namespace ServerHubJogoMVC.Server.Model
{
    public class Partida
    {
        public List<Jogada> Jogadas { get; set; } = new List<Jogada>();
        public List<Jogador> Participantes { get; set; } = new List<Jogador>();

        public Jogador Mandante()
        {
            if (Participantes.Count == 2)
                return Participantes[0];

            return null;
        }
        public Jogador Visitante()
        {
            if (Participantes.Count == 2)
                return Participantes[1];
            return null;
        }
        public bool PartidaIniciada { get; set; }

        public PartidaResult RealizouUmaJogada(string idJogador, string posicao)
        {
            var result = new PartidaResult();
            var jogador = Participantes.First(x => x.Id == idJogador);

            if (!jogador.EhMinhaVez)
                result.Mensagem.Add("Não foi possível realizar a jogada. Favor aguardar a sua vez.");
            else
            {
                Jogadas.Add(new Jogada { IdJogador = idJogador, Posicao = posicao });
                PassarAVezDeJogar(idJogador);
                result.Sucesso = true;
            }
            return result;
        }

        private void PassarAVezDeJogar(string idUltimoQueJogou)
        {
            var ultimoJogador = Participantes.First(x => x.Id == idUltimoQueJogou);
            var proximoJogador = Participantes.First(x => x.Id != idUltimoQueJogou);

            ultimoJogador.EhMinhaVez = false;
            proximoJogador.EhMinhaVez = true;

        }


    }
}