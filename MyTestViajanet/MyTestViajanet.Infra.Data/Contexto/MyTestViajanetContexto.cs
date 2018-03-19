namespace MyTestViajanet.Infra.Data.Contexto
{
    using Microsoft.EntityFrameworkCore;
    using MyTestViajanet.Infra.Data.EntityConfig;
    using MyTestViajaNet.DomainService.Entities;
    using System;
    using System.Linq;

    public class MyTestViajanetContexto : DbContext
    {
        public MyTestViajanetContexto()
             : base()
        {
        }

        public DbSet<BatePapoOnline> BatePapoOnline { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // for the other conventions, we do a metadata model loop
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                    entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BatePapoOnlineConfiguration());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (local);Initial Catalog = MyTestViajanet;Integrated Security=True");
        }
    }
}