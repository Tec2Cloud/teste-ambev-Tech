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

        public Venda CriarVenda(Guid clienteId, string nomeCliente, Guid filialId, string nomeFilial, List<ItemVenda> itensDto)
        {
            var venda = new Venda(clienteId, nomeCliente, filialId, nomeFilial);

            foreach (var itemDto in itensDto)
            {
                venda.AdicionarItem(itemDto.ProdutoId, itemDto.NomeProduto, itemDto.ValorUnitario, itemDto.Quantidade, itemDto.Desconto);
            }

            _vendaRepository.Adicionar(venda);

            return venda;
        }

    public Venda AtualizarVenda(Guid vendaId, List<ItemVenda> novosItensDto)
    {
        var venda = _vendaRepository.ObterPorId(vendaId);
        if (venda == null)
            throw new Exception("Venda não encontrada.");

        venda.Itens.Clear();

        foreach (var itemDto in novosItensDto)
        {
            venda.AdicionarItem(itemDto.ProdutoId, itemDto.NomeProduto, itemDto.ValorUnitario, itemDto.Quantidade, itemDto.Desconto);
        }

        venda.RecalcularValorTotal();

        _vendaRepository.Atualizar(venda);

        return venda;
    }

    public void CancelarVenda(Guid vendaId)
        {
            var venda = _vendaRepository.ObterPorId(vendaId);
            if (venda == null)
                throw new Exception("Venda não encontrada.");

            venda.CancelarVenda();

            _vendaRepository.Atualizar(venda);
        }

        public IEnumerable<Venda> ObterTodasVendas()
        {
            return _vendaRepository.ObterTodas();
        }

        public Venda? ObterVendaPorId(Guid vendaId)
        {
            return _vendaRepository.ObterPorId(vendaId);
        }
    }
}
