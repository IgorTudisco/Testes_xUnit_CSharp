using JornadaMilhas.Dados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace JornadaMilhas.Test.Integracao;

public class ContextoFixture
{
    public JornadaMilhasContext Context { get; }

    // Configuração do container do SQL Server para testes de integração no Docker
    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public ContextoFixture()
    {
        var opitions = new DbContextOptionsBuilder<JornadaMilhasContext>()
            .UseSqlServer(_sqlContainer.GetConnectionString())
            .Options;

        Context = new JornadaMilhasContext(opitions);
    }
}
