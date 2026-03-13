using JornadaMilhas.Dados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integracao;

public class OfertaViagemDalRecuperaPorId : IClassFixture<ContextoFixture>
{
    private readonly JornadaMilhasContext _context;

    public OfertaViagemDalRecuperaPorId(ITestOutputHelper outputHelper, ContextoFixture contextoFixture)
    {
        // Utilizando a conexão compartilhada para teste de percistencia
        _context = contextoFixture.Context;
        outputHelper.WriteLine($"Conexão {_context.GetHashCode()} com o banco de dados estabelecida com sucesso.");
    }

    [Fact]
    public void RecuperaOfertaViagemPorId()
    {
        // arrage
        var dal = new OfertaViagemDAL(_context);

        // act
        var oferta = dal.RecuperarPorId(-1);

        // assert
        Assert.Null(oferta);
    }

}