using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IVendaRepository
    {
        Venda? ObterPorId(Guid id);
        IEnumerable<Venda> ObterTodas();
        void Adicionar(Venda venda);
        void Atualizar(Venda venda);
        void Remover(Venda venda);
    }
}
