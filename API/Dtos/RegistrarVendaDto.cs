namespace API.Dtos
{
    public class RegistrarVendaDto
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string? NomeCliente { get; set; }
        public Guid FilialId { get; set; }
        public string? NomeFilial { get; set; }
        public List<ItemVendaDto>? Itens { get; set; }
    }
}
