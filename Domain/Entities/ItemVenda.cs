using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ItemVenda
    {
        public Guid ProdutoId { get; private set; } 
        public string NomeProduto { get; private set; } 
        public decimal ValorUnitario { get; private set; } 
        public int Quantidade { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotalItem { get; private set; } 

        public ItemVenda(Guid produtoId, string nomeProduto, decimal valorUnitario, int quantidade, decimal desconto)
        {
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
            Desconto = desconto;
            RecalcularValorTotalItem();
        }

        private void RecalcularValorTotalItem()
        {
            ValorTotalItem = (ValorUnitario * Quantidade) - Desconto;
        }
    }
}
