using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.Entities;

namespace TelefoniaApi.Data.MapContext
{
    public class DDDMap : IEntityTypeConfiguration<DDD>
    {
        public void Configure(EntityTypeBuilder<DDD> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Numero);
            builder.HasOne(x => x.Operadora);
        }
    }
}
