using Bogus;
using JornadaMilhas.Dados;
using JornadaMilhasV1.Modelos;
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
    public JornadaMilhasContext? Context { get; private set; }

    // Configuração atualizada para criar um container do SQL Server para testes de integração no Docker
    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
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

    public void GeraDadosFaker()
    {
        Periodo periodo = new PeriodoDataBuilder().Build();

        Rota rota = new RotaDataBuilder().Build();

        var fakerOferta = new Faker<OfertaViagem>()
            .CustomInstantiator(f => new OfertaViagem(
                new RotaDataBuilder().Build(),
                new PeriodoDataBuilder().Build(),
                100 * f.Random.Int(1, 100))
            )
            .RuleFor(o => o.Desconto, f => 40)
            .RuleFor(o => o.Ativa, f => true);

        var listaOfertaFaker = fakerOferta.Generate(200);

        Context!.OfertasViagem.AddRange(listaOfertaFaker);
        Context.SaveChanges();
    }

    /// <summary>
    /// Garante o isolamento entre os testes, limpando o estado do banco de dados 
    /// para evitar efeitos colaterais e garantir a determinística dos resultados.
    /// </summary>
    public async Task ClearDb()
    {
        // Optamos por ExecuteSqlRawAsync em vez de RemoveRange para ignorar o Change Tracker do EF Core.
        // Isso reduz o consumo de memória e melhora a performance ao evitar o carregamento dos registros 
        // antes da exclusão (ideal para ambientes de CI/CD ou Docker com latência de I/O).
        await Context!.Database.ExecuteSqlRawAsync("DELETE FROM OfertasViagem");
        await Context.Database.ExecuteSqlRawAsync("DELETE FROM Rota");
    }
}