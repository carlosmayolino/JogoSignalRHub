using ServerHubJogoMVC.Server.Model;
using ServerHubJogoMVC.Server.Model.BDHub;
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

        public List<Jogador> InciarPartida()
        {
            var jogadores = _context.ObterJogador().Where(x => !x.PartidaEmAndamento).Take(2).ToList();
            if (jogadores.Count() == 2)
            {
                jogadores.ForEach(x => x.PartidaEmAndamento = true);

                return jogadores;
            }

            return null;
        }
    }

}
