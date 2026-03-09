using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Fact]
        public void RetornaPrecoAtualQuandoAplicadoDesconto() // Usando TDD
        {
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double precoOriginal = 1000.0;
            double desconto = 20.0;
            double precoComDesconto = precoOriginal - desconto;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            oferta.Desconto = desconto;

            Assert.Equal(precoComDesconto, oferta.Preco);
        }

        [Fact]
        public void RetornaPrecoComDescontoMaximo()
        {
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(new DateTime(2024, 1, 15), new DateTime(2024, 1, 20));
            double precoOriginal = 1000.0;
            double desconto = 1500.0; // Desconto maior que o preço original
            double precoComDesconto = 300.0;

            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            oferta.Desconto = desconto;

            Assert.Equal(precoComDesconto, oferta.Preco);
        }
    }
}
