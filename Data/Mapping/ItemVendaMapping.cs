using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ItemVendaMapping : IEntityTypeConfiguration<ItemVenda>
    {
        public void Configure(EntityTypeBuilder<ItemVenda> builder)
        {
            builder.ToTable("ItemVenda");
            builder.HasKey(e => e.ProdutoId);
            builder.Property(e => e.NomeProduto).IsRequired().HasMaxLength(200);
            builder.Property(e => e.ValorUnitario).IsRequired();
            builder.Property(e => e.Quantidade).IsRequired();
            builder.Property(e => e.Desconto).IsRequired();
            builder.Property(e => e.ValorTotalItem).IsRequired();

        }
    }
}
