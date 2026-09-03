<h1>Sistema de Controle de Acesso para Residências - Atualização</h1>
<p>Repositório público para demonstração técnica de portfólio. Este projeto é uma evolução de uma solução de automação/IoT desenvolvida originalmente em Node.js (2021), agora totalmente reestruturada no ecossistema .NET para garantir alta performance, escalabilidade e adoção de boas práticas de Engenharia de Software. Junto ao .NET, tem-se a utilização do MySQL para banco de dados, e conteinerização em Docker, rodando em servidor local</p>
<h3>🛠️ Nota de Desenvolvimento:</h3>
<p>O projeto original é gerenciado ativamente dentro da plataforma Azure DevOps, utilizando Azure Boards (metodologias ágeis) e pipelines. Este repositório no GitHub funciona como um espelho público do que foi e vem sendo desenvolvido por lá</p>

<h2>Resumo</h2>
<p>A constante evolução da tecnologia impactou radicalmente o estilo de vida da sociedade 
atual, trazendo aspectos como a velocidade, o estímulo à criatividade e o conforto. Neste
contexto está o advento da Domótica - aliado ao conceito de IoT, em que dispositivos são
integrados para a satisfação das necessidades básicas de segurança, comunicação, gestão
energética e conforto de uma habitação.</p>
<p>Alinhado aos conceitos de Domótica e Internet das Coisas (IoT), o sistema automatiza e gerencia o controle de acesso residencial via biometria utilizando componentes de baixo custo. A arquitetura foi desenhada para integrar hardware e software de forma eficiente, permitindo fácil escalabilidade para novos métodos de autenticação e interfaces de gerenciamento (Desktop/Mobile).</p>

<h2>⚙️ Arquitetura e Funcionamento Básico</h2>
<p>O ecossistema é baseado no modelo cliente-servidor e arquitetura orientada a eventos, composto por:</p>
<ul>
  <li>Servidor Central (.NET): API RESTful responsável pelas regras de negócio, persistência de dados e operações administrativas (CRUD) via requisições HTTP.</li>
  <li>Mensageria (MQTT): Protocolo leve adotado para a comunicação assíncrona, direta e de baixa latência entre o servidor e o hardware.</li>
  <li>Firmware/Hardware (ESP32): Microcontrolador responsável pela interface direta com o sensor biométrico, atuando como cliente MQTT.</li>  
</ul>

<h2>⚙️ Tecnologias e Ferramentas Utilizadas</h2>
<ul>
  <li>Back-End: .NET / C#</li>
  <li>Mensageria: MQTT</li>
  <li>Infraestrutura: Docker (Conteinerização do ambiente de desenvolvimento e banco de dados em servidor local</li>  
  <li>Gestão e Versionamento: Azure DevOps (Repositório original, Git e gerenciamento de tarefas)</li>  
</ul>
