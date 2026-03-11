using JornadaMilhas.Dados;
using JornadaMilhasV1.Modelos;
using Microsoft.EntityFrameworkCore;

namespace JornadaMilhas.Test.Integracao;

public class OfertaViagemDalAdicionar
{
    [Fact]
    public void RegistraOfertaNoBanco()
    {
        // arrage
        Rota rota = new Rota("Curitiba", "São Paulo");
        Periodo periodo = new Periodo(new DateTime(2024, 05, 20), new DateTime(2026, 05, 30));
        double preco = 150.00;

        var opitions = new DbContextOptionsBuilder<JornadaMilhasContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JornadaMilhas;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .Options;

        var context = new JornadaMilhasContext(opitions);

        var ofertaViagem = new OfertaViagem(rota, periodo, preco);
        var dal = new OfertaViagemDAL(context);

        // act
        dal.Adicionar(ofertaViagem);

        // assert
        var ofertaIncluida = dal.RecuperarPorId(ofertaViagem.Id);

        Assert.NotNull(ofertaIncluida);
        Assert.Equal(ofertaIncluida.Preco, preco, 0.001);

    }
}