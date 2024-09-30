using Bogus;
using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace Tests.UnitTests.Business.Services;
public class VendaServiceTest
{
    private readonly IVendaRepository _vendaRepositoryMock;
    private readonly IVendaService _vendaService;
    private readonly Faker<Venda> _vendaFaker;
    private readonly Faker<ItemVenda> _itemVendaFaker;

    public VendaServiceTest()
    {
        _vendaRepositoryMock = Substitute.For<IVendaRepository>();
        _vendaService = new VendaService(_vendaRepositoryMock);

        _itemVendaFaker = new Faker<ItemVenda>()
            .CustomInstantiator(f => new ItemVenda(
                f.Random.Guid(),
                f.Commerce.ProductName(),
                f.Random.Decimal(10, 100),
                f.Random.Int(1, 10),
                f.Random.Decimal(0, 50)
            ));

        _vendaFaker = new Faker<Venda>()
            .CustomInstantiator(f => new Venda(
                f.Random.Guid(),
                f.Person.FullName,
                f.Random.Guid(),
                f.Company.CompanyName()
            ));
    }

    [Fact]
    public async Task GivenValidVendaData_WhenRegistrarVendaIsCalled_ThenItShouldReturnSuccess()
    {
        var clienteId = Guid.NewGuid();
        var filialId = Guid.NewGuid();
        var itens = _itemVendaFaker.Generate(2);

        _vendaRepositoryMock.RegistrarAsync(Arg.Any<Venda>()).Returns(clienteId);

        var result = await _vendaService.RegistrarVenda(clienteId, "Cliente1", filialId, "Filial1", itens);

        result.IsSuccess.Should().BeTrue();
        await _vendaRepositoryMock.Received(1).RegistrarAsync(Arg.Any<Venda>());
    }

    [Fact]
    public async Task GivenVendaDoesNotExist_WhenAtualizarVendaIsCalled_ThenItShouldReturnFailure()
    {
        var vendaId = Guid.NewGuid();
        var novosItens = _itemVendaFaker.Generate(2);

        _vendaRepositoryMock.ObterPorIdAsync(vendaId).Returns((Venda)null);

        var result = await _vendaService.AtualizarVenda(vendaId, novosItens);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Venda não encontrada.");
    }

    [Fact]
    public async Task GivenVendaExists_WhenCancelarVendaIsCalled_ThenItShouldReturnSuccess()
    {
        var venda = _vendaFaker.Generate();
        _vendaRepositoryMock.ObterPorIdAsync(venda.Id).Returns(venda);

        var result = await _vendaService.CancelarVenda(venda.Id);

        result.IsSuccess.Should().BeTrue();
        await _vendaRepositoryMock.Received(1).AtualizarAsync(venda);
    }

    [Fact]
    public async Task GivenVendasExist_WhenObterTodasVendasIsCalled_ThenItShouldReturnAllVend()
    {
        var vendas = _vendaFaker.Generate(2);
        _vendaRepositoryMock.ObterTodasAsync().Returns(vendas);

        var result = await _vendaService.ObterTodasVendas();

        result.Should().HaveCount(2);
        await _vendaRepositoryMock.Received(1).ObterTodasAsync();
    }

    [Fact]
    public async Task GivenVendaExists_WhenObterVendaPorIdIsCalled_ThenItShouldReturnVenda()
    {
        var venda = _vendaFaker.Generate();
        _vendaRepositoryMock.ObterPorIdAsync(venda.Id).Returns(venda);

        var result = await _vendaService.ObterVendaPorId(venda.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(venda.Id);
        await _vendaRepositoryMock.Received(1).ObterPorIdAsync(venda.Id);
    }
}
