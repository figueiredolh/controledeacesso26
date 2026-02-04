using ControleDeAcesso26.Domain.Base;

namespace ControleDeAcesso26.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public required string Nome { get; set; }
        public required string Apelido { get; set; }
    }
}
