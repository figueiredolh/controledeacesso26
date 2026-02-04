namespace ControleDeAcesso26.Domain.Base
{
    public class EntityBase
    {
        public long Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;
    }
}
