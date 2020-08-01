using ServerHubJogoMVC.Server.Model;
using ServerHubJogoMVC.Server.Model.BDHub;
using ServerHubJogoMVC.Server.Model.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerHubJogoMVC.Server.Repository
{
    public class HubServerRepository
    {
        private readonly ContextHub _context;
        public HubServerRepository(ContextHub cont)
        {
            _context = cont;
        }
        public void AguadarNaFila(string id, string nome)
        {
            _context.AddJogador(new Jogador { Id = id, Apelido = nome });
        }

        public PartidaResult InciarPartida()
        {
            var jogadores = _context.ObterJogador().Where(x => !x.PartidaEmAndamento).Take(2).ToList();
            var partida = _context.AddPartida(jogadores);
            return new PartidaResult { Sucesso = partida.PartidaIniciada, _Partida = partida };
            
        }
    }

}
