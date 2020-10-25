using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Data.MapContext;
using TelefoniaApi.Domain.Entities;

namespace TelefoniaApi.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {

        }

        public DbSet<Plano> Planos { get; set; }
        public DbSet<DDD> DDDs { get; set; }
        public DbSet<Operadora> Operadoras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlanoMap());
            modelBuilder.ApplyConfiguration(new DDDMap());
            modelBuilder.ApplyConfiguration(new OperadoraMap());
        }
    }
}
