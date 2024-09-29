using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly List<Venda> _vendas = new List<Venda>();

        public Venda? ObterPorId(Guid id)
        {
            return _vendas.FirstOrDefault(v => v.Id == id);
        }

        public IEnumerable<Venda> ObterTodas()
        {
            return _vendas;
        }

        public void Adicionar(Venda venda)
        {
            _vendas.Add(venda);
        }

        public void Atualizar(Venda venda)
        {
            var vendaExistente = ObterPorId(venda.Id);
            if (vendaExistente != null)
            {
                _vendas.Remove(vendaExistente);
                _vendas.Add(venda);
            }
        }

        public void Remover(Venda venda)
        {
            _vendas.Remove(venda);
        }
    }
}
