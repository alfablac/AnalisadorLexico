# AnalisadorLexico
Analisador Léxico de identificadores, palavras reservadas e comentários usando expressões regulares.

![alt text](https://i.imgur.com/VKNQTwc.png "Exemplo")

# Dependências
- Visual Studio

# Funcionamento e informações
Esse aplicativo retorna comentários, identificadores (nomes de variáveis, funções, classes, métodos, etc.) e palavras reservadas encontradas no código que é escrito no RichTextBox à esquerda da aplicação. Ao clicar em Analisar, será colocado no primeiro ListBox os lexemas encontradas no código referentes àqueles que a aplicação está construída para mostrar. O segundo ListBox mostra a posição e a classificação destes lexemas. A terceira ListBox aponta como erro todos os outros lexemas (literais numéricos, operadores, novas linhas, cadeias de caracteres, separadores, etc.) não considerados como comentários, identificadores e palavras reservadas.

Testador de expressões regulares usado na codificação do programa:
http://regexstorm.net/tester

Lexemas são tratados por casos nas capturas das expressões regulares.
Funções de sincronia entre listboxes e sincronia entre o índice do lexema e o richtextbox estão incluídos.

# To-do list
- ~~Criar tabela de identificadores~~
- ~~Remanejar comentários para outro lugar (não são lexemas)
- Expandir pra outros lexemas


