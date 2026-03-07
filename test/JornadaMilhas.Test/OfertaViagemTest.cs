using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemTest
    {
        [Fact]
        public void TestandoOpertaVaida()
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
        public void TestandoOpertaComRotaNula()
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
        public void TestandoOpertaDataVoltaMenor()
        {
            // Seguindo o padrão AAA (Arrange, Act, Assert)
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(new DateTime(2026, 1, 15), new DateTime(2024, 10, 20));
            double valor = 500.0;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, valor);

            Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void TestandoOpertaValorZerado()
        {
            // Cenário - Arrange
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double valor = 00.0;

            // Ação - Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, valor);

            // Verificação - Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }
    }
}