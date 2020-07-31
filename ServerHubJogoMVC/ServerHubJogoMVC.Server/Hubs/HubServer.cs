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
            var teste = Context.ConnectionId;
            Clients.Clients(teste).SendAsync("MensagemOla", "Seja bem vindo!");
            Clients.All.SendAsync("MensagemOla", "Seja bem vindo!");
            return base.OnConnectedAsync();
        }

        public async Task AguadarNaFila(string nome)
        {
            var idCon = Context.ConnectionId;
            //await Clients.Clients(idCon).SendAsync("EntrouNaFila", $"Iae. {nome}. Você está na fila aguardando seu oponente");
            _repo.AguadarNaFila(idCon, nome);
            await Clients.Caller.SendAsync("EntrouNaFila", $"Iae. {nome}. Você está na fila aguardando seu oponente");
            await IniciarPartida();
        }

        public async Task IniciarPartida()
        {
            var jogadores = _repo.InciarPartida();
            if (jogadores != null)
                await Clients.Clients(jogadores[0].Id, jogadores[1].Id).SendAsync("PartidaIniciada", jogadores[0].Apelido, jogadores[1].Apelido);

        }
    }
}
