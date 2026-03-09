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
        public void RetornaArtistaDaMusica()
        {
            // Arrange
            string nome = "Música Teste";
            string artista = "Artista Teste";
            Musica musica = new Musica(nome)
            {
                Artista = artista
            };
            // Act
            string resultado = musica.Artista;
            // Assert
            Assert.Equal(resultado, musica.Artista);
        }

        [Fact]
        public void RetornaArtistaDesconhecidoQuandoArtistaForNulo()
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
    }
}
