using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqliteDataAccess.Models;

namespace SqliteDataAccess.Configs;

class ProductionMessageConfig : IEntityTypeConfiguration<ProductionMessage>
{
    public void Configure(EntityTypeBuilder<ProductionMessage> builder)
    {
        builder.ToTable("ProductionMessages");
    }
}
