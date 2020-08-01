using System;
using System.Collections.Generic;
using System.Text;

namespace ServerHubJogoMVC.Server.Model.BDHub
{
    public class ContextHub
    {
        private static List<Jogador> _dbSetJogador = new List<Jogador>();
        private static List<Partida> _dbSetPartidas = new List<Partida>();

        public void AddJogador(Jogador instancia)
        {
            _dbSetJogador.Add(instancia);
        }
        public List<Jogador> ObterJogador()
        {
            return _dbSetJogador;
        }

        public Partida AddPartida(List<Jogador> jogadores)
        {
            var _partida = new Partida();

            if (jogadores.Count == 2)
            {
                foreach (var item in jogadores)
                {
                    item.PartidaEmAndamento = true;
                    _partida.Participantes.Add(item);
                }
                _dbSetPartidas.Add(_partida);
                _partida.PartidaIniciada = true;
                return _partida;
            }
            return _partida;
        }

    }
}
