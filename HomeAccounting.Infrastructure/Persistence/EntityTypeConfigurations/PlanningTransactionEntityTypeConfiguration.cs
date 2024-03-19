using HomeAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeAccounting.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class PlanningTransactionEntityTypeConfiguration : IEntityTypeConfiguration<PlanningTransaction>
    {
        public void Configure(EntityTypeBuilder<PlanningTransaction> builder)
        {
            builder.ToTable("PlanningTransaction");
        }
    }
}
