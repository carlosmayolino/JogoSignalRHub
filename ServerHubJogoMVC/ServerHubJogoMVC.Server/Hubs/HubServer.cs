using Microsoft.AspNetCore.SignalR;
using ServerHubJogoMVC.Server.Repository;
using System;
using System.Threading.Tasks;

namespace ServerHubJogoMVC.Server.Hubs
{
    public class HubServer : Hub
    {
        private readonly HubServerRepository _repo;

        public HubServer(HubServerRepository repo)
        {
            _repo = repo;
        }
        public override Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;
            Clients.Clients(id).SendAsync("MensagemOla", "Seja bem vindo!");
            Clients.All.SendAsync("MensagemOla", "Seja bem vindo!");
            return base.OnConnectedAsync();
        }

        public async Task AguadarNaFila(string nome)
        {
            var idCon = Context.ConnectionId;
            _repo.AguadarNaFila(idCon, nome);
            await Clients.Caller.SendAsync("EntrouNaFila", $"Iae. {nome}. Você está na fila aguardando seu oponente");
            await IniciarPartida();
        }

        public async Task IniciarPartida()
        {
            var result = _repo.InciarPartida();
            if (result._Partida.PartidaIniciada)
            {
                await Clients.Clients(
                    result._Partida.Mandante().Id, 
                    result._Partida.Visitante().Id)
                    .SendAsync("PartidaIniciada", result._Partida.Mandante().Apelido, result._Partida.Visitante().Apelido);
            }
        }

        public async Task Jogar(string posicao)
        {
            await Clients.Caller.SendAsync("JogadaAprovada", "XXXXXX", posicao);
        }
    }
}

