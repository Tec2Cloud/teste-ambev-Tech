using API.Dtos;
using Domain.Entities;

namespace API.Extensions
{
    public static class MappingExtensions
    {
        public static List<ItemVenda> ToItemVendaList(this List<ItemVendaDto> itemVendaDtos)
        {
            return itemVendaDtos?.Select(dto => new ItemVenda(
                dto.ProdutoId,
                dto.NomeProduto,
                dto.ValorUnitario,
                dto.Quantidade,
                dto.Desconto
            )).ToList() ?? new List<ItemVenda>();
        }
    }
}
