using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyTestViajaNet.DomainService.Entities;

namespace MyTestViajanet.Infra.Data.EntityConfig
{
    class BatePapoOnlineConfiguration : IEntityTypeConfiguration<BatePapoOnline>
    {
        public void Configure(EntityTypeBuilder<BatePapoOnline> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.LugaresDisponiveis).HasMaxLength(200);
            builder.Property(c => c.PessoasOnline).HasMaxLength(200);
        }
    }
}
