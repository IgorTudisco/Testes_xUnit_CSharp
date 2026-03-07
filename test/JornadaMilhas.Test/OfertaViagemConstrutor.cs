using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    // Mudando o nome da classe de teste para refletir o cenário testado, seguindo a convenção de nomenclatura de testes
    public class OfertaViagemConstrutor
    {
        [Fact]
        public void RetornaDadosValidosQuandoDadosValidos()
        {
            // Cenário - Arrange
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo (new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double valor = 500.0;
            var validacao = true;

            // Ação - Act
            OfertaViagem oferta = new OfertaViagem (rota, periodo, valor);

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

        [Fact]
        public void RetornaMensagemDeErroQuandoADataDeIdaForMaiorQueAdataDeVolta() // Padronizando a nomenclatura do teste para refletir o cenário testado
        {
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(new DateTime(2026, 1, 15), new DateTime(2024, 10, 20));
            double valor = 500.0;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, valor);

            Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroQuandoOPrecoForIgualAZero()
        {
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double valor = 00.0;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, valor);

            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }

        [Fact]
        public void RetornaMensagemDeErroQuandoOPrecoForMenorQueZero()
        {
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo (new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double valor = -100.0;

            OfertaViagem oferta = new OfertaViagem (rota, periodo, valor);

            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }
    }
}


// Artigos:

/*
 * https://learn.microsoft.com/pt-br/dotnet/core/testing/unit-testing-best-practices
 * https://ardalis.com/mastering-unit-tests-dotnet-best-practices-naming-conventions/
 */