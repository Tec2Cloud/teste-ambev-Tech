namespace Domain.Entities
{
    public class Venda
    {

        public Guid Id { get; private set; }
        public string NumeroVenda { get; private set; } 
        public DateTime DataVenda { get; private set; } 
        public Guid ClienteId { get; private set; }
        public string NomeCliente { get; private set; }
        public decimal ValorTotal { get; private set; } 
        public Guid FilialId { get; private set; } 
        public string NomeFilial { get; private set; } 
        public bool Cancelada { get; private set; }

        public List<ItemVenda> Itens { get; private set; } = new List<ItemVenda>();

        public Venda(Guid clienteId, string nomeCliente, Guid filialId, string nomeFilial)
        {
            Id = Guid.NewGuid();
            NumeroVenda = GerarNumeroVenda();
            DataVenda = DateTime.Now;
            ClienteId = clienteId;
            NomeCliente = nomeCliente;
            FilialId = filialId;
            NomeFilial = nomeFilial;
            Cancelada = false;
        }

        public void AdicionarItem(Guid produtoId, string nomeProduto, decimal valorUnitario, int quantidade, decimal desconto)
        {
            var item = new ItemVenda(produtoId, nomeProduto, valorUnitario, quantidade, desconto);
            Itens.Add(item);
            RecalcularValorTotal();
        }

        public void CancelarVenda()
        {
            Cancelada = true;
        }

        public void RecalcularValorTotal()
        {
            ValorTotal = 0;
            foreach (var item in Itens)
            {
                ValorTotal += item.ValorTotalItem;
            }
        }

        private string GerarNumeroVenda()
        {
            return $"VEN-{DateTime.Now.Ticks}";
        }
    }
}
