using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IVendaService
    {
        Venda CriarVenda(Guid clienteId, string nomeCliente, Guid filialId, string nomeFilial, List<ItemVenda> itensDto);
        Venda AtualizarVenda(Guid vendaId, List<ItemVenda> novosItensDto);
        void CancelarVenda(Guid vendaId);
        IEnumerable<Venda> ObterTodasVendas();
        Venda? ObterVendaPorId(Guid vendaId);
    }
}
