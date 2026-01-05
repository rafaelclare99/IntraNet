using System.ComponentModel.DataAnnotations;

namespace IntraNet.Models
{
    public class Avisos
    {
        public int AvisosId { get; set; }
        public string Titulo { get; set; } = "";
        public string Mensagem { get; set; } = "";
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public string Setor { get; set; } // null = geral
        public string AutorId { get; set; } = "";
    }
}
