using HomeAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeAccounting.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");

            builder.HasData(new Admin()
            {
                Id = 1,
                UserName = "Master",
                PasswordHash = "WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=",
                FullName = "Xolmatov Raximjon",
                Gender = Domain.Enums.Gender.Male,
                PhoneNumber = "994779050",
                Email = "xolmatovabdurahim1987@gmail.com"
            });
        }
    }
}
