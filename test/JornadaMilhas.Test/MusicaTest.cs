using JornadaMilhas.Modelos;
using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class MusicaTest
    {
        [Fact]
        public void TesteNomeInicializadoCorretamente()
        {
            // Arrange
            string nome = "Música Teste";

            // Act
            Musica musica = new Musica(nome);

            // Assert
            Assert.Equal(nome, musica.Nome);
        }


        [Fact]
        public void TesteIdInicializadoCorretamente()
        {
            // Arrange
            string nome = "Música Teste";
            int id = 13;

            // Act
            Musica musica = new Musica(nome) { Id = id };

            // Assert
            Assert.Equal(id, musica.Id);
        }

        [Fact]
        public void TesteToString()
        {
            // Arrange
            int id = 1;
            string nome = "Música Teste";
            Musica musica = new Musica(nome);
            musica.Id = id;
            string toStringEsperado = @$"Id: {id} Nome: {nome}";

            // Act
            string resultado = musica.ToString();

            // Assert
            Assert.Equal(toStringEsperado, resultado);
        }
    }
}