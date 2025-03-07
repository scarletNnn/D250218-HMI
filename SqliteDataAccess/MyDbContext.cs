using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SqliteDataAccess.Models;

namespace SqliteDataAccess;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<AlertMessage> AlertMessages { get; set; }

    public string CreateDateTime { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite("Data Source=HMI.db")
            .ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactoryDesignTimeSupport>();
        optionsBuilder.UseLazyLoadingProxies(); //调用指示 EF Core 实现延迟加载，因此从父实体访问时，会自动加载子实体。
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //从当前程序集中加载所有的配置类
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
