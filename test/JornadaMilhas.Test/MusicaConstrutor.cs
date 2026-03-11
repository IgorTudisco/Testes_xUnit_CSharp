using Bogus;
using JornadaMilhas.Modelos;

// classe de exercícios
namespace JornadaMilhas.Test
{
    public class MusicaConstrutor
    {
        [Theory]
        [InlineData("Música Teste")]
        [InlineData("Outra Música")]
        [InlineData("Mais uma Música")]
        public void InicializaNomeCorretamenteQuandoCadastraNovaMusica(string nome)
        {
            // Act
            Musica musica = new Musica(nome);

            // Assert
            Assert.Equal(nome, musica.Nome);
        }


        [Theory]
        [InlineData("Música Teste", "Nome: Música Teste")]
        [InlineData("Outra Música", "Nome: Outra Música")]
        [InlineData("Mais uma Música", "Nome: Mais uma Música")]
        public void ExibeDadosDaMusicaCorretamenteQuandoChamadoMetodoExibeFichaTecnica
            (string nome, string saidaEsperada)
        {
            // Arrange
            Musica musica = new Musica(nome);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            musica.ExibirFichaTecnica();
            string saidaAtual = stringWriter.ToString().Trim();

            // Assert
            Assert.Equal(saidaEsperada, saidaAtual);
        }

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
        public void RetornaMusicaEhValidaComDadosFakes()
        {
            // Arrage
            var musicaFaker = new Faker<Musica>()
                .CustomInstantiator(m =>
                {
                    string nome = m.Lorem.Word();
                    Musica musica = new Musica(nome);
                    musica.Artista = m.Person.FullName;
                    musica.AnoLancamento = m.Date.Past(50).Year;
                    return musica;
                });

            var musica = musicaFaker.Generate(100);
            var listaMusicas = new List<Musica>(musica);

            // Act
            bool todasMusicasValidas = listaMusicas.All(m => !string.IsNullOrEmpty(m.Nome) && m.AnoLancamento > 1900 && !string.IsNullOrEmpty(m.Artista));
            bool resultado = true;

            // Assert
            Assert.Equal(resultado, todasMusicasValidas);

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
            var nome = new Faker().Name.FirstName();
            var musica = new Musica(nome) { AnoLancamento = -1 };

            // Act
            var anoLancamento = musica.AnoLancamento;

            // Assert
            Assert.Null(anoLancamento);
        }
    }
}