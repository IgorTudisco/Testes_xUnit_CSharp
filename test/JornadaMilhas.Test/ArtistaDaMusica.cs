using Bogus;
using JornadaMilhas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class ArtistaDaMusica
    {
        [Fact]
        public void RetornaToStringCorretamenteQuandoMusicaEhCadastrada()
        {
            // Arrange
            var faker = new Faker();
            var id = faker.Random.Int();
            var nome = faker.Name.FullName();
            var saidaEsperada = $"Id: {id} Nome: {nome}";
            var musica = new Musica(nome) { Id = id };

            // Act
            var result = musica.ToString();

            // Assert
            Assert.Equal(saidaEsperada, result);
        }

        [Fact]
        public void RetornaArtistaDesconhecidoQuandoValorInseridoEhNulo()
        {
            string nome = "Música Teste";
            string? artista = null;
            Musica musica = new Musica(nome)
            {
                Artista = artista
            };

            string resultado = "Artista desconhecido";

            Assert.Equal(resultado, musica.Artista);
        }

        [Fact]
        public void RetornaArtistaDesconecidoQuandoValorInseridoEhVazio()
        {
            string nome = "Música Teste";
            string? artista = "";
            Musica musica = new Musica(nome)
            {
                Artista = artista
            };
            string resultado = "Artista desconhecido";
            Assert.Equal(resultado, musica.Artista);
        }

        [Fact]
        public void RetornaArtistaDesconhecidoQuandoInseridoDadoNuloNoArtista()
        {
            // Arrange
            var nome = new Faker().Name.FullName();
            var musica = new Musica(nome) { Artista = null };

            // Act
            var artista = musica.Artista;

            // Assert
            Assert.Equal("Artista desconhecido", artista);
        }

        [Fact]
        public void RetornoAnoDeLancamentoNuloQuandoValorInseridoMenorQueZero()
        {
            // Arrange
            var nome = new Faker().Name.FullName();
            var musica = new Musica(nome) { AnoLancamento = -1 };

            // Act
            var anoLancamento = musica.AnoLancamento;

            // Assert
            Assert.Null(anoLancamento);
        }
    }
}
