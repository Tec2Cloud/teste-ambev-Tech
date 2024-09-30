namespace API.Dtos
{
    public class ItemVendaDto
    {
        public Guid ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal Desconto { get; set; }
    }
}
