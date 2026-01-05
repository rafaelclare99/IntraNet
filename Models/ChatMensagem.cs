namespace IntraNet.Models
{
    public class ChatMensagem
    {
        public int Id { get; set; }

        public string Usuario { get; set; } = string.Empty;

        public string Mensagem { get; set; } = string.Empty;

        public DateTime DataEnvio { get; set; }
    }
}
