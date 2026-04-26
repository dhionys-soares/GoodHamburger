using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.Mappings;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedNever();
        
        builder.Property(o => o.Total)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Navigation(o => o.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(o => o.Items, item =>
        {
            item.ToTable("OrderItems");

            item.WithOwner()
                .HasForeignKey("OrderId");

            item.Property<Guid>("Id");

            item.HasKey("Id");

            item.Property(i => i.Quantity)
                .IsRequired();

            item.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey("ProductId")
                .IsRequired();

            item.Navigation(i => i.Product)
                .AutoInclude();
        });
    }
}