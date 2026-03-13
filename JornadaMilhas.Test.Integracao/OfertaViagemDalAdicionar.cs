using JornadaMilhas.Dados;
using JornadaMilhasV1.Modelos;
using Microsoft.EntityFrameworkCore;

namespace JornadaMilhas.Test.Integracao;

public class OfertaViagemDalAdicionar
{
    private readonly JornadaMilhasContext _context;

    public OfertaViagemDalAdicionar()
    {
        var opitions = new DbContextOptionsBuilder<JornadaMilhasContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JornadaMilhas;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .Options;

        _context = new JornadaMilhasContext(opitions);
    }

    [Fact]
    public void RegistraOfertaNoBanco()
    {
        // arrage
        Rota rota = new Rota("Curitiba", "São Paulo");
        Periodo periodo = new Periodo(new DateTime(2024, 05, 20), new DateTime(2026, 05, 30));
        double preco = 150.00;

        var ofertaViagem = new OfertaViagem(rota, periodo, preco);
        var dal = new OfertaViagemDAL(_context);

        // act
        dal.Adicionar(ofertaViagem);

        // assert
        var ofertaIncluida = dal.RecuperarPorId(ofertaViagem.Id);

        Assert.NotNull(ofertaIncluida);
        Assert.Equal(ofertaIncluida.Preco, preco, 0.001);

    }

    [Fact]
    public void RegistraInformacoesNoBancoCorretas()
    {
        // arrage
        Rota rota = new Rota("Curitiba", "São Paulo");
        Periodo periodo = new Periodo(new DateTime(2024, 05, 20), new DateTime(2026, 05, 30));
        double preco = 150.00;

        var ofertaViagem = new OfertaViagem(rota, periodo, preco);
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