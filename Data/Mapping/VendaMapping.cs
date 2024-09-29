using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Data.Mapping
{
    public class VendaMapping : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("Venda");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.NumeroVenda).IsRequired().HasMaxLength(50);
            builder.Property(e => e.DataVenda).IsRequired();
            builder.Property(e => e.NomeCliente).IsRequired().HasMaxLength(200);
            builder.Property(e => e.NomeFilial).IsRequired().HasMaxLength(200);

            builder.HasMany(e => e.Itens)
                  .WithOne()
                  .HasForeignKey("VendaId")
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
