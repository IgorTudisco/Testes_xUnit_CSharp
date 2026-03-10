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

            OfertaViagem oferta = new OfertaViagem(rota!, periodo, valor);

            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoSaoInvalidos()
        {
            int quantidadeEsperada = 3;
            Rota? rota = null;
            Periodo periodo = new Periodo(new DateTime(2026, 1, 15), new DateTime(2024, 1, 20));
            double valor = -100.0;
            
            OfertaViagem oferta = new OfertaViagem(rota!, periodo, valor);           

            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }

        [Fact]
        public void RetornaMensagemDeErroQuandoOPrecoForZero()
        {
            // Seguindo o padrão AAA (Arrange, Act, Assert)
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double valor = 0.0;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, valor);

            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaToStringEhValido()
        {
            // Seguindo o padrão AAA (Arrange, Act, Assert)
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double valor = 0.0;
            string resultado = $"Origem: {rota.Origem}, Destino: {rota.Destino}, Data de Ida: {periodo.DataInicial.ToShortDateString()}, Data de Volta: {periodo.DataFinal.ToShortDateString()}, Preço: {valor:C}";

            OfertaViagem oferta = new OfertaViagem(rota, periodo, valor);

            Assert.Equal(resultado ,oferta.ToString());
        }
    }
}
