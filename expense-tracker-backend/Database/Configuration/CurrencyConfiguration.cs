using expense_tracker_backend.Domain.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace expense_tracker_backend.Database.Configuration
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(currency => currency.Id);

            builder
                .HasMany(currency => currency.Categories)
                .WithOne()
                .HasForeignKey(category => category.CurrencyId);
        }
    }
}
