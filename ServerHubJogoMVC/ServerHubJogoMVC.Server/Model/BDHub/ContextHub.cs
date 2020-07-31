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

        public void AddPartida(Jogador a, Jogador b)
        {
            a.PartidaEmAndamento = true;
            b.PartidaEmAndamento = true;
            _dbSetPartidas.Add(new Partida { Jogadores = new List<Jogador> { a, b } });
        }

    }
}
