using JornadaMilhas.Dados;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integracao;

[Collection(nameof(ContextoCollection))]
public class RecuperarTodasOfertaViagem
{
    private readonly JornadaMilhasContext _context;

    public RecuperarTodasOfertaViagem(ITestOutputHelper outputHelper, ContextoFixture contextoFixture)
    {
        // Utilizando a conexão compartilhada para teste de percistencia
        _context = contextoFixture.Context;
        outputHelper.WriteLine($"Conexão {_context.GetHashCode()} com o banco de dados estabelecida com sucesso.");
    }

    [Fact]
    public void RecuperaTodasAsOfertasViagem()
    {
        // arrage
        var dal = new OfertaViagemDAL(_context);

        // act
        var ofertas = dal.RecuperarTodas();

        // assert
        Assert.NotNull(ofertas);
    }
}
