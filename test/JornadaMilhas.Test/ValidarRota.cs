using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class ValidarRota
    {
        [Fact]
        public void RetornaRotaEhValida()
        {
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");

            var resultado = "São Paulo";

            Assert.Equal(resultado, rota.Origem);
        }

        [Theory]
        [InlineData("", "Rio de Janeiro")]
        [InlineData(null, "Rio de Janeiro")]
        public void RetornaMensagemDeErroQuandoOrigimInvalida(string? origim,string? destino)
        {
            Rota rota = new Rota(origim!, destino!);

            var resultado = "A rota não pode possuir uma origem nula ou vazia.";

            Assert.Contains(resultado, rota.Erros.Sumario);
        }

        [Theory]
        [InlineData("São Paulo", "")]
        [InlineData("São Paulo", null)]
        public void RetornaMensagemDeErroQuandoDestinoInvalido(string? origim, string? destino)
        {
            Rota rota = new Rota(origim!, destino!);

            var resultado = "A rota não pode possuir um destino nulo ou vazio.";

            Assert.Contains(resultado, rota.Erros.Sumario);
        }
    }
}
