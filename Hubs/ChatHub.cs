using IntraNet.Data;
using IntraNet.Models;
using Microsoft.AspNetCore.SignalR;

namespace IntraNet.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task EnviarMensagem(string mensagem)
        {
            var usuario = Context.User.Identity?.Name ?? "Usuário";

            var chatMensagem = new ChatMensagem
            {
                Usuario = usuario,
                Mensagem = mensagem,
                DataEnvio = DateTime.Now
            };

            _context.ChatMensagens.Add(chatMensagem);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync(
                "ReceberMensagem",
                usuario,
                mensagem,
                chatMensagem.DataEnvio.ToString("HH:mm")
            );
        }
    }
}
