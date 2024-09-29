using Business.Interfaces;
using Data.Interfaces;
using Domain.Entities;

namespace Business.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository; 

        public VendaService(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public async Task<Venda> RegistrarVenda(Guid clienteId, string nomeCliente, Guid filialId, string nomeFilial, List<ItemVenda> itensDto)
        {
            var venda = new Venda(clienteId, nomeCliente, filialId, nomeFilial);

            foreach (var itemDto in itensDto)
            {
                venda.AdicionarItem(itemDto.ProdutoId, itemDto.NomeProduto, itemDto.ValorUnitario, itemDto.Quantidade, itemDto.Desconto);
            }

            await _vendaRepository.RegistrarAsync(venda);

            return venda;
        }

    public async Task<Venda> AtualizarVenda(Guid vendaId, List<ItemVenda> novosItensDto)
    {
        var venda = await _vendaRepository.ObterPorIdAsync(vendaId);
        if (venda == null)
            throw new Exception("Venda não encontrada.");

        venda.Itens.Clear();

        foreach (var itemDto in novosItensDto)
        {
            venda.AdicionarItem(itemDto.ProdutoId, itemDto.NomeProduto, itemDto.ValorUnitario, itemDto.Quantidade, itemDto.Desconto);
        }

        venda.RecalcularValorTotal();

        await _vendaRepository.AtualizarAsync(venda);

        return venda;
    }

    public async Task CancelarVenda(Guid vendaId)
        {
            var venda = await _vendaRepository.ObterPorIdAsync(vendaId);
            if (venda == null)
                throw new Exception("Venda não encontrada.");

            venda.CancelarVenda();

            await _vendaRepository.AtualizarAsync(venda);
        }

        public async Task<IEnumerable<Venda>> ObterTodasVendas()
        {
            return await _vendaRepository.ObterTodasAsync();
        }

        public async Task<Venda> ObterVendaPorId(Guid vendaId)
        {
            return await _vendaRepository.ObterPorIdAsync(vendaId);
        }
    }
}
