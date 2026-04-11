using ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Verificar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces
{
    public interface IVerificarBiometriaUsuarioUseCase
    {
        public Task Execute(MqttVerificarBiometriaUsuarioReceivedPayloadJson payload, string topic);
    }
}
