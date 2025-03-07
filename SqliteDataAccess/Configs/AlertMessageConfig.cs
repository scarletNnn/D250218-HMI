using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqliteDataAccess.DbAccess;
using SqliteDataAccess.Models;

namespace SqliteDataAccess.Configs;

class AlertMessageConfig : IEntityTypeConfiguration<AlertMessage>
{
    public void Configure(EntityTypeBuilder<AlertMessage> builder)
    {
        var name = DataAccess.SearchTime;
        builder.ToTable(name);
    }
}
