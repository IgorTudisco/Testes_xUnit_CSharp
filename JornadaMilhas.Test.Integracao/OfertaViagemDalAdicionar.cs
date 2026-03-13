using JornadaMilhas.Dados;
using JornadaMilhasV1.Modelos;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integracao;

[Collection(nameof(ContextoCollection))]
public class OfertaViagemDalAdicionar
{
    private readonly JornadaMilhasContext _context;

    public OfertaViagemDalAdicionar(ITestOutputHelper outputHelper, ContextoFixture contextoFixture)
    {
        // Utilizando o ContextoFixture para garantir que o contexto seja compartilhado entre os testes
        _context = contextoFixture.Context;
        outputHelper.WriteLine($"Conexão {_context.GetHashCode()} com o banco de dados estabelecida com sucesso.");
    }

    public OfertaViagem CriarOfertaComValorfixo()
    {
        Rota rota = new Rota("Curitiba", "São Paulo");
        Periodo periodo = new Periodo(new DateTime(2024, 05, 20), new DateTime(2026, 05, 30));
        double preco = 150.00;

        var ofertaViagem = new OfertaViagem(rota, periodo, preco);
        return ofertaViagem;
    }

    [Fact]
    public void RegistraOfertaNoBanco()
    {
        // arrage
        var ofertaViagem = CriarOfertaComValorfixo();
        var dal = new OfertaViagemDAL(_context);

        // act
        dal.Adicionar(ofertaViagem);

        // assert
        var ofertaIncluida = dal.RecuperarPorId(ofertaViagem.Id);

        Assert.NotNull(ofertaIncluida);
        Assert.Equal(ofertaIncluida.Preco, ofertaViagem.Preco, 0.001);
    }

    [Fact]
    public void RegistraInformacoesNoBancoCorretamente()
    {
        // arrage
        var ofertaViagem = CriarOfertaComValorfixo();
        var dal = new OfertaViagemDAL(_context);

        // act
        dal.Adicionar(ofertaViagem);

        // assert
        var ofertaIncluida = dal.RecuperarPorId(ofertaViagem.Id);

        Assert.Equal(ofertaIncluida!.Rota.Origem, ofertaViagem.Rota.Origem);
        Assert.Equal(ofertaIncluida.Rota.Destino, ofertaViagem.Rota.Destino);
        Assert.Equal(ofertaIncluida.Periodo.DataInicial, ofertaViagem.Periodo.DataInicial);
        Assert.Equal(ofertaIncluida.Periodo.DataFinal, ofertaViagem.Periodo.DataFinal);
        Assert.Equal(ofertaIncluida.Preco, ofertaViagem.Preco, 0.001);
    }
}