using Domain.Entities;

namespace Data.Interfaces
{
    public interface IVendaRepository
    {
        Task<Guid> RegistrarAsync(Venda venda);
        Task AtualizarAsync(Venda venda);
        Task<Venda> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Venda>> ObterTodasAsync();
        Task CancelarAsync(Venda venda);
    }
}
