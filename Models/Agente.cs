namespace IntraNet.Models
{
    public class Agente
    {
        public string AgenteId { get; set; }

        public string AgenteName { get; set; }

        public int SetoresId { get; set; }
        public virtual Setores Setores { get; set; }
    }
}
