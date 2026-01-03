using System.ComponentModel.DataAnnotations;

namespace IntraNet.Models
{
    public class Avisos
    {
        public int AvisosId { get; set; }

        [Required]
        [StringLength(120)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Conteudo { get; set; } = string.Empty;

        public DateTime DataPublicacao { get; set; } = DateTime.Now;

        public string Autor { get; set; } = string.Empty;
    }
}
