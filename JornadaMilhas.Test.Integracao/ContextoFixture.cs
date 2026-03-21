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

        Context.OfertasViagem.AddRange(listaOfertaFaker);
        Context.SaveChanges();
    }

    // Método para limpar o banco de dados, para garantir que cada teste use um banco limpo e evitar interferências entre os testes
    public async Task ClearDb()
    {
        // Usando ExecuteSqlRawAsync para executar comandos SQL diretamente no banco de dados, garantindo que todas as ofertas de viagem sejam removidas após cada teste, mantendo a integridade dos testes de integração. Essa abordagem é mais eficiente que usar o método RemoveRange, especialmente quando lidamos com um grande número de registros, pois evita a necessidade de carregar os dados na memória antes de excluí-los. Ex: Context.RemoveRange(Context.OfertasViagem); e Context.SaveChanges();

        // Forma correta: libera a thread enquanto o banco trabalha.
        await Context.Database.ExecuteSqlRawAsync("DELETE FROM OfertasViagem");

        // Forma perigosa: trava a thread e espera "na marra".
        Context.Database.ExecuteSqlRawAsync("DELETE FROM OfertasViagem").Wait();
    }
}