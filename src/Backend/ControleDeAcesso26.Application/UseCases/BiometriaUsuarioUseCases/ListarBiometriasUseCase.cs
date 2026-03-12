using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;
using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario;
using Mapster;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases
{
    public class ListarBiometriasUseCase : IListarBiometriasUseCase
    {
        private readonly ITemplateBiometriaReadOnlyRepository _templateBiometriaReadOnlyRepository;
        public ListarBiometriasUseCase(ITemplateBiometriaReadOnlyRepository templateBiometriaReadOnlyRepository)
        {
            _templateBiometriaReadOnlyRepository = templateBiometriaReadOnlyRepository;
        }

        public async Task<List<ResponseListarBiometriasUsuarioJson>> Execute(int? idUsuario = null, int paginaAtual = 1)
        {
            var templatesBanco = await _templateBiometriaReadOnlyRepository.BuscarTemplates(idUsuario, paginaAtual);
            var templatesDto = templatesBanco.Adapt<List<ResponseListarBiometriasUsuarioJson>>();

            return templatesDto;
        }

    }
}
