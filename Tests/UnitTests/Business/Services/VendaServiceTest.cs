using Bogus;
using Business.Events;
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
    private readonly IEventPublisher _eventPublisherMock; 
    private readonly Faker<Venda> _vendaFaker;
    private readonly Faker<ItemVenda> _itemVendaFaker;

    public VendaServiceTest()
    {
        _vendaRepositoryMock = Substitute.For<IVendaRepository>();
        _eventPublisherMock = Substitute.For<IEventPublisher>();
        _vendaService = new VendaService(_vendaRepositoryMock, _eventPublisherMock);

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
    public async Task GivenValidVendaData_WhenRegistrarVendaIsCalled_ThenItShouldPublishVendaRegistradaEvent()
    {
        var clienteId = Guid.NewGuid();
        var filialId = Guid.NewGuid();
        var itens = _itemVendaFaker.Generate(2);

        _vendaRepositoryMock.RegistrarAsync(Arg.Any<Venda>()).Returns(clienteId);

        var result = await _vendaService.RegistrarVenda(clienteId, "Cliente1", filialId, "Filial1", itens);

        result.IsSuccess.Should().BeTrue();
        await _vendaRepositoryMock.Received(1).RegistrarAsync(Arg.Any<Venda>());
        _eventPublisherMock.Received(1).Publish("VendaRegistrada", Arg.Is<VendaRegistradaEvent>(e =>
            e.VendaId == result.Value.Id && e.Cliente == "Cliente1"));
    }

    [Fact]
    public async Task GivenValidVendaData_WhenAtualizarVendaIsCalled_ThenItShouldPublishVendaAlteradaEvent()
    {
        var venda = _vendaFaker.Generate();
        var novosItens = _itemVendaFaker.Generate(2);

        _vendaRepositoryMock.ObterPorIdAsync(venda.Id).Returns(venda);

        var result = await _vendaService.AtualizarVenda(venda.Id, novosItens);

        result.IsSuccess.Should().BeTrue();
        await _vendaRepositoryMock.Received(1).AtualizarAsync(venda);
        _eventPublisherMock.Received(1).Publish("VendaAlterada", Arg.Is<VendaAlteradaEvent>(e =>
            e.VendaId == venda.Id));
    }
    [Fact]
    public async Task GivenVendaExists_WhenCancelarVendaIsCalled_ThenItShouldPublishVendaCanceladaEvent()
    {
        var venda = _vendaFaker.Generate();
        _vendaRepositoryMock.ObterPorIdAsync(venda.Id).Returns(venda);

        var result = await _vendaService.CancelarVenda(venda.Id);

        result.IsSuccess.Should().BeTrue();
        await _vendaRepositoryMock.Received(1).AtualizarAsync(venda);
        _eventPublisherMock.Received(1).Publish("VendaCancelada", Arg.Is<VendaCanceladaEvent>(e =>
            e.VendaId == venda.Id));
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
