using ControleDeAcesso26.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeAcesso26.Domain.Entities
{
    public class TemplateBiometriaUsuario : EntityBase
    {
        public required int IdSensor1 { get; set; }
        public int? IdSensor2 { get; set; }
        public required byte[] Template { get; set; }
        public required long IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; } = null!;
    }
}
