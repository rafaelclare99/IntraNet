namespace IntraNet.Models
{
    public class Setores
    {
        public int SetoresId { get; set; }

        public string SetoresName { get; set; }

        public string SetoresDescricao { get; set; }

        public List<Agente> Agentes { get; set; }
    }
}
