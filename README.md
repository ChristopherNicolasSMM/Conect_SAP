# Exemplo para conectar com SAP

Estou disponibilizando este exemplo com o próposito de auxiliar integrações de maneira eficiente.
Testes e validações de ferramentas de maneira mais agil. 
Podendo obter uma abstração inicial para a conexão com o SAP e outras linguagens. 

Assim que abrir o projeto, verifique o caminho das bibliotecas abaixo, exclua elas da referencia do projeto.

![alt text](https://github.com/ChristopherNicolasSMM/Conect_SAP/blob/master/imagens/img1.png?raw=true)



Agora entre na pasta abaixo e adicione as bibliotecas no projeto, detalhe estou usando x64.

![alt text](https://github.com/ChristopherNicolasSMM/Conect_SAP/blob/master/imagens/img2.png?raw=true)

Após este passo estar realizado precisa limpar e reconstruir o projeto.
Feito isso abra o SAP ao qual quer se conectar para criar uma RFC.

Nome da RFC a ser chamada!

![alt text](https://github.com/ChristopherNicolasSMM/Conect_SAP/blob/master/imagens/img3.png?raw=true)


Vá na SE37 e crie a RFC ZSD_RFC_INTEGRACAO
![alt text](https://github.com/ChristopherNicolasSMM/Conect_SAP/blob/master/imagens/img4.png?raw=true)

Feito isso adicione os parametros conforme indicado e crie o tipo corespondente de tabela.

![alt text](https://github.com/ChristopherNicolasSMM/Conect_SAP/blob/master/imagens/img4.png?raw=true)


#Agora é sucesso só ativar tudo no SAP, e ser feliz.
