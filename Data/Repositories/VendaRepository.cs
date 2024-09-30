using Data.Context;
using Data.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        protected readonly VendasDbContext Context;
        protected readonly DbSet<Venda> Vendas;

        public VendaRepository(VendasDbContext context)
        {
            Context = context;
            Vendas = Context.Set<Venda>();
        }
        public async Task<Venda> ObterPorIdAsync(Guid id)
        {
            return await Vendas.AsNoTracking()
                .Include(v => v.Itens)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Venda>> ObterTodasAsync()
        {
            return await Vendas.AsNoTracking().Include(v => v.Itens).ToListAsync();
        }

        public async Task<Guid> RegistrarAsync(Venda venda)
        {
            await Vendas.AddAsync(venda);
            await Context.SaveChangesAsync();
            return venda.Id;
        }

        public async Task AtualizarAsync(Venda venda)
        {
            Vendas.Update(venda);
            await Context.SaveChangesAsync();
        }
    }
}
