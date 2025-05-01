java -jar .\antlr-4.13.2-complete.jar -Dlanguage=CSharp -no-listener -visitor .\Yappembler.g4
Remove-Item .\Yappembler.interp
Remove-Item .\Yappembler.tokens
Remove-Item .\YappemblerLexer.interp
Remove-Item .\YappemblerLexer.tokens
