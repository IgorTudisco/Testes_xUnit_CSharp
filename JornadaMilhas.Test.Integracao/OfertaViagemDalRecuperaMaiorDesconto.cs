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
public class OfertaViagemDalRecuperaMaiorDesconto
{
    private readonly JornadaMilhasContext _contexto;
    private readonly ContextoFixture fixture;

    public OfertaViagemDalRecuperaMaiorDesconto(ContextoFixture fixture)
    {
        _contexto = fixture.Context;
        this.fixture = fixture;
    }

    [Fact]
    // Refatorando o código para usar o contexto de teste e o gerenciador de ofertas, garantindo que a oferta específica seja retornada corretamente do banco de dados.
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
}
