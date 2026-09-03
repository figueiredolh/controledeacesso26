<h1>Sistema de Controle de Acesso para Residências - Atualização</h1>
<p>Atualização de Projeto de Automação/IoT de controle de acesso voltado à ambientes residenciais, feito inicialmente em Node/Express, em 2021. Sistema Back-End em andamento, construído em .NET, com o uso de MySQL para banco de dados, e conteinerização em Docker, testado continuamente em servidor local</p>
<p>Projeto vem sendo desenvolvido na plataforma Azure DevOps, utilizando as principais ferramentas que auxiliam e aceleram o processo de desenvolvimento. Esse repositório é uma cópia do que vem sendo desenvolvido por lá. Por esse motivo, algumas coisas podem estar incompletas ou desatualizadas</p>

<h2>Resumo</h2>
<p>A constante evolução da tecnologia impactou radicalmente o estilo de vida da sociedade 
atual, trazendo aspectos como a velocidade, o estímulo à criatividade e o conforto. Neste
contexto está o advento da Domótica - aliado ao conceito de IoT, em que dispositivos são
integrados para a satisfação das necessidades básicas de segurança, comunicação, gestão
energética e conforto de uma habitação.</p>
<p>O projeto em questão visa desenvolver um sistema de controle de acesso, utilizando componentes de baixo custo, via Biometria, para ambientes residenciais, em que se utilizará uma interface gráfica para gerenciamento.</p>

<h2>Funcionamento Básico do Sistema</h2>
<p>O projeto tem como objetivo fazer a integração de
um hardware a um software, ou seja, realizar a integração entre um sistema de controle de
acesso via sensor biométrico - com a possibilidade de escalar para outras formas de acesso - à uma plataforma de gerenciamento. Para atingir esse objetivo, o modelo
adotado está baseado na presença de um servidor central em .NET para o atendimento de requisições
HTTP, para tarefas mais administrativas como CRUD, bem como para recebimento e publicação de mensagens via MQTT, para uma comunicação mais direta com o sensor biométrico</p>
<p> Os clientes - neste modelo - são o microcontrolador ESP 32,
que conversa diretamente com o sensor biométrico, e a plataforma de gerenciamento,
que poderá ser acessada via dispositivo Desktop ou Mobile.</p>
  
<!-- <p>A integração dos componentes pode ser vista na figura abaixo:</p> -->

<!-- <div width="900px" height="499px"><img src="https://user-images.githubusercontent.com/74880337/148081800-74e6ca6c-7df4-402f-b801-e70ef7234631.jpg"></div> -->

<footer>
</footer>
