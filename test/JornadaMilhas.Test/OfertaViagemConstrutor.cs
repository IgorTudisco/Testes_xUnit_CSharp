using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    // Mudando o nome da classe de teste para refletir o cenário testado, seguindo a convenção de nomenclatura de testes
    public class OfertaViagemConstrutor
    {
        // Utilizando o atributo [Theory] para testar múltiplos cenários de validação do construtor da classe OfertaViagem, seguindo as melhores práticas de testes unitários
        [Theory]
        [InlineData("São Paulo", "Rio de Janeiro", "2024-01-15", "2024-01-20", 500.0, true)] // Validação de um cenário válido
        [InlineData("São Paulo", "Rio de Janeiro", "2024-01-15", "2024-01-20", 0.0, false)] // Validação de um cenário onde o preço é igual a zero, o que deve ser inválido
        [InlineData("São Paulo", "Rio de Janeiro", "2024-01-15", "2024-01-20", -100.0, false)] // Validação de um cenário onde o preço é negativo, o que deve ser inválido
        [InlineData("São Paulo", "Rio de Janeiro", "2026-01-15", "2024-01-10", 500.0, false)] // Validação de um cenário onde a data de ida é maior que a data de volta, o que deve ser inválido        
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origim, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            // Cenário - Arrange
            Rota rota = new Rota(origim, destino);
            Periodo periodo = new Periodo (DateTime.Parse(dataIda), DateTime.Parse(dataVolta));            

            // Ação - Act
            OfertaViagem oferta = new OfertaViagem (rota, periodo, preco);

            // Verificação - Assert
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDeRotaOuPeriodoInvalidosQuandoARotaForNula()
        {
            // Seguindo o padrão AAA (Arrange, Act, Assert)
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double valor = 500.0;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, valor);

            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }
    }
}


// Artigos:

/*
 * https://learn.microsoft.com/pt-br/dotnet/core/testing/unit-testing-best-practices
 * https://ardalis.com/mastering-unit-tests-dotnet-best-practices-naming-conventions/
 * https://learn.microsoft.com/pt-br/visualstudio/test/quick-start-test-driven-development-with-test-explorer?view=vs-2022
 * https://learn.microsoft.com/pt-br/dotnet/core/testing/unit-testing-best-practices#validate-private-methods-by-unit-testing-public-methods
 */

// Documentação:

/*
 * https://learn.microsoft.com/pt-br/dotnet/fundamentals/reflection/overview
 * https://xunit.net/?tabs=cs
  
   Outras Natoções
    [TestFixture] - uma classe que contém um conjunto de testes de unidade relacionados;
    [Test] - utilizada para identificar testes distintos dentro de uma mesma classe de teste;
    [Ignore] - utilizada para ignorar um teste específico durante a execução;
    [Collection] - utilizada para agrupar testes em coleções específicas.
 */