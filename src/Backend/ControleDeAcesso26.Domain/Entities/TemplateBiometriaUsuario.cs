using ControleDeAcesso26.Domain.Base;

namespace ControleDeAcesso26.Domain.Entities
{
    public class TemplateBiometriaUsuario : EntityBase
    {
        public required long IdSensor1 { get; set; }
        public long? IdSensor2 { get; set; }
        public required byte[] Template { get; set; }
        public required long IdUsuario { get; set; }
    }
}
