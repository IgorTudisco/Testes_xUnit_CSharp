using JornadaMilhas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class AnoDeLancamentoDaMudica
    {
        [Fact]
        public void RetornaAnoDeLancamentoDaMusica()
        {
            // Arrange
            string nome = "Música Teste";
            int anoLancamento = 2020;
            Musica musica = new Musica(nome);
            musica.AnoLancamento = anoLancamento;
            // Act
            int resultado = 2020;
            // Assert
            Assert.Equal(anoLancamento, resultado);
        }

        [Fact]
        public void RetonaNuloQuandoOAnoForIgualAZero()
        {
            string nome = "Música Teste";
            int? anoInvalido = 0;
            Musica musica = new(nome)
            {
                AnoLancamento = anoInvalido
            };

            Assert.Null(musica.AnoLancamento);
        }

        [Fact]
        public void RetonaNuloQuandoOAnoForNegativo()
        {
            string nome = "Música Teste";
            int? anoInvalido = -1522;
            Musica musica = new Musica(nome);

            musica.AnoLancamento = anoInvalido;

            Assert.Null(musica.AnoLancamento);
        }
    }
}
