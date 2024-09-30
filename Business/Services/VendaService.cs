using Business.Common;
using Business.Events;
using Business.Interfaces;
using Data.Interfaces;
using Domain.Entities;

namespace Business.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IEventPublisher _eventPublisher;


        public VendaService(IVendaRepository vendaRepository, IEventPublisher eventPublisher)
        {
            _vendaRepository = vendaRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<Result<Venda>> RegistrarVenda(Guid clienteId, string nomeCliente, Guid filialId, string nomeFilial, List<ItemVenda> itensDto)
        {
            var venda = new Venda(clienteId, nomeCliente, filialId, nomeFilial);

            foreach (var itemDto in itensDto)
            {
                venda.AdicionarItem(itemDto.ProdutoId, itemDto.NomeProduto, itemDto.ValorUnitario, itemDto.Quantidade, itemDto.Desconto);
            }

            await _vendaRepository.RegistrarAsync(venda);

            PublicarEvento("VendaRegistrada", new VendaRegistradaEvent
            {
                VendaId = venda.Id,
                Data = DateTime.Now,
                Cliente = venda.NomeCliente
            });

            return Result<Venda>.Success(venda);
        }

        public async Task<Result<Venda>> AtualizarVenda(Guid vendaId, List<ItemVenda> novosItensDto)
        {
            var venda = await _vendaRepository.ObterPorIdAsync(vendaId);
            if (venda == null)
                return Result<Venda>.Failure("Venda não encontrada.");

            venda.Itens.Clear();

            foreach (var itemDto in novosItensDto)
            {
                venda.AdicionarItem(itemDto.ProdutoId, itemDto.NomeProduto, itemDto.ValorUnitario, itemDto.Quantidade, itemDto.Desconto);
            }

            venda.RecalcularValorTotal();

            await _vendaRepository.AtualizarAsync(venda);

            PublicarEvento("VendaAlterada", new VendaAlteradaEvent
            {
                VendaId = venda.Id,
                Data = DateTime.Now
            });

            return Result<Venda>.Success(venda);
        }

        public async Task<Result> CancelarVenda(Guid vendaId)
        {
            var venda = await _vendaRepository.ObterPorIdAsync(vendaId);
            if (venda == null)
                return Result.Failure("Venda não encontrada.");

            venda.CancelarVenda();

            await _vendaRepository.AtualizarAsync(venda);

            PublicarEvento("VendaCancelada", new VendaCanceladaEvent
            {
                VendaId = venda.Id,
                Data = DateTime.Now
            });

            return Result.Success();
        }

        public async Task<IEnumerable<Venda>> ObterTodasVendas()
        {
            return await _vendaRepository.ObterTodasAsync();
        }

        public async Task<Venda> ObterVendaPorId(Guid vendaId)
        {
            return await _vendaRepository.ObterPorIdAsync(vendaId);
        }

        private void PublicarEvento(string nomeEvento, object evento)
        {
            _eventPublisher.Publish(nomeEvento, evento);
        }
    }
}
