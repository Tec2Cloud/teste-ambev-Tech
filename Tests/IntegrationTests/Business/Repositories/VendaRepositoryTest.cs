using Microsoft.Data.Sqlite;
using FluentAssertions;
using Data.Context;
using Data.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Tests.IntegrationTests.Business.Services;
public class VendaRepositoryTests : IAsyncLifetime
{
    private SqliteConnection _sqliteConnection;
    private VendasDbContext _dbContext;
    private VendaRepository _vendaRepository;

    public async Task InitializeAsync()
    {
        _sqliteConnection = new SqliteConnection("DataSource=:memory:");
        await _sqliteConnection.OpenAsync();

        var options = new DbContextOptionsBuilder<VendasDbContext>()
            .UseSqlite(_sqliteConnection)
            .Options;

        _dbContext = new VendasDbContext(options);
        await _dbContext.Database.EnsureCreatedAsync();

        _vendaRepository = new VendaRepository(_dbContext);
    }

    public async Task DisposeAsync()
    {
        await _sqliteConnection.CloseAsync();
        _dbContext.Dispose();
    }

    [Fact]
    public async Task GivenValidVendaData_WhenRegistrarVendaIsCalled_ThenItShouldBeSavedInSqlite()
    {
        var venda = new Venda(Guid.NewGuid(), "Cliente Teste", Guid.NewGuid(), "Filial Teste");
        venda.AdicionarItem(Guid.NewGuid(), "Produto1", 10.00m, 1, 0);
        venda.AdicionarItem(Guid.NewGuid(), "Produto2", 20.00m, 2, 0);

        await _vendaRepository.RegistrarAsync(venda);
        var result = await _vendaRepository.ObterPorIdAsync(venda.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(venda.Id);
        result.Itens.Should().HaveCount(2);
    }
}
