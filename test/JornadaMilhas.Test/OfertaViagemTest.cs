using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemTest
    {
        [Fact]
        public void TestandoOpertaVaida()
        {
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo (new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double valor = 500.0;
            var validacao = true;

            OfertaViagem oferta = new OfertaViagem (rota, periodo, valor);

            Assert.Equal(validacao, oferta.EhValido);
        }
    }
}