using Business.Common;
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
        Task<Result<Venda>> RegistrarVenda(Guid clienteId, string nomeCliente, Guid filialId, string nomeFilial, List<ItemVenda> itensDto);
        Task<Result<Venda>> AtualizarVenda(Guid vendaId, List<ItemVenda> novosItensDto);
        Task<Result> CancelarVenda(Guid vendaId);
        Task<IEnumerable<Venda>> ObterTodasVendas();
        Task<Venda> ObterVendaPorId(Guid vendaId);
    }
}
