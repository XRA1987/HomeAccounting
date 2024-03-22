using HomeAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Admin> Admins { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<ExistingTransaction> ExistingTransactions { get; set; }
        DbSet<PlanningTransaction> PlanningTransactions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
