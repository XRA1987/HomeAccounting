using HomeAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeAccounting.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class ExistingTransactionEntityTypeConfiguration : IEntityTypeConfiguration<ExistingTransaction>
    {
        public void Configure(EntityTypeBuilder<ExistingTransaction> builder)
        {
            builder.ToTable("ExistingTransaction");
        }
    }
}
