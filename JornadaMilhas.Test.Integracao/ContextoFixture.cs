using JornadaMilhas.Dados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace JornadaMilhas.Test.Integracao;

// fix: A arumando o problema de criação assíncrona do contexto
public class ContextoFixture : IAsyncLifetime
{
    public JornadaMilhasContext Context { get; private set; }

    // Configuração do container do SQL Server para testes de integração no Docker
    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public async Task InitializeAsync()
    {
        _sqlContainer.StartAsync().Wait(); // Inicia o container do SQL Server
        var opitions = new DbContextOptionsBuilder<JornadaMilhasContext>()
            .UseSqlServer(_sqlContainer.GetConnectionString())
            .Options;

        Context = new JornadaMilhasContext(opitions);
        Context.Database.Migrate(); // Aplica as migrate para criar o banco de dados e as tabelas necessárias para os testes
    }

    public async Task DisposeAsync()
    {
        await _sqlContainer.StopAsync(); // Encerra o container do SQL Server após os testes
    }
}
