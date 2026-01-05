namespace IntraNet.Models
{
    public class Processo
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Setor { get; set; } = string.Empty;

        public string? ArquivoPath { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
