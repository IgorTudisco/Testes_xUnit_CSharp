using Bogus;
using JornadaMilhas.Dados;
using JornadaMilhasV1.Gerenciador;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test.Integracao;

[Collection(nameof(ContextoCollection))]
public class OfertaViagemDalRecuperaMaiorDesconto : IDisposable
{
    private readonly JornadaMilhasContext _contexto;
    private readonly ContextoFixture fixture;

    public OfertaViagemDalRecuperaMaiorDesconto(ContextoFixture fixture)
    {
        _contexto = fixture.Context;
        this.fixture = fixture;
    }

    [Fact]
    public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto40()
    {
        //arrange
        var rota = new RotaDataBuilder() { Origem = "Bhaia", Destino = "São Paulo" };
        var periodo = new PeriodoDataBuilder() { DataInicial = new DateTime(2024, 7, 1) }.Build();

        fixture.GeraDadosFaker();

        var ofertaEscolhida = new OfertaViagem(rota, periodo, 80)
        {
            Desconto = 40,
            Ativa = true
        };

        var dal = new OfertaViagemDAL(_contexto);
        dal.Adicionar(ofertaEscolhida);

        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
        var precoEsperado = 40;

        //act
        var oferta = dal.RecuperaMaiorDesconto(filtro);

        //assert
        Assert.NotNull(oferta);
        Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
    }

    [Fact]
    public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto60()
    {
        //arrange
        var rota = new RotaDataBuilder() { Origem = "Bhaia", Destino = "São Paulo" };
        var periodo = new PeriodoDataBuilder() { DataInicial = new DateTime(2024, 7, 1) }.Build();

        fixture.GeraDadosFaker();

        var ofertaEscolhida = new OfertaViagem(rota, periodo, 80)
        {
            Desconto = 60,
            Ativa = true
        };

        var dal = new OfertaViagemDAL(_contexto);
        dal.Adicionar(ofertaEscolhida);

        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
        var precoEsperado = 20;

        //act
        var oferta = dal.RecuperaMaiorDesconto(filtro);

        //assert
        Assert.NotNull(oferta);
        Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
    }

    // Implementação do método Dispose para limpar o banco de dados após os testes, garantindo que cada teste seja executado em uma base limpa
    // Assim usando o conceito de TearDown para evitar que os dados de um teste interfiram nos outros, mantendo a integridade dos testes de integração.
    public void Dispose()
    {
        fixture.ClearDb().Wait();
    }
}
