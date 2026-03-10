using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test;

public class GerenciadorDeOFertasRecuperaMaiorDesconto
{
    [Fact]
    public void RetornaOfertaNulaQuandoListaEstaVazia()
    {
        // arrange
        var lista = new List<OfertaViagem>();
        var gerenciador = new GerenciadorDeOfertas(lista);
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

        // act
        var oferta = gerenciador.RecuperaMaiorDesconto(filtro);

        // assert
         Assert.Null(oferta);
    }

    
}
