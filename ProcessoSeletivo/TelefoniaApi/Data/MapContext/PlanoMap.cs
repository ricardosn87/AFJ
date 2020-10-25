using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.Entities;

namespace TelefoniaApi.Data.MapContext
{
    public class PlanoMap : IEntityTypeConfiguration<Plano>
    {
        public void Configure(EntityTypeBuilder<Plano> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CodigoPlano);
            builder.Property(x => x.Minutos);
            builder.Property(x => x.FranquiaInternet);
            builder.Property(x => x.Valor);      
            builder.Property(x => x.Tipo);
            builder.HasOne(x => x.DDD);
        }
    }
}
