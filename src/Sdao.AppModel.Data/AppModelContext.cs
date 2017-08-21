using Microsoft.EntityFrameworkCore;
using Sdao.AppModel.Model.Entities;
using System;
using System.Linq;

namespace Sdao.AppModel.Data
{
    public class AppModelContext : DbContext
    {
        /// <summary>
        /// 容器表
        /// </summary>
        public DbSet<Container> Containers { get; set; }
        
        public AppModelContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

         foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
         {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
         }

         //relationships
         modelBuilder.HasSequence<long>("t_container_seq", schema: "shared")
         .StartsAt(1000)
         .IncrementsBy(1);

         modelBuilder.Entity<Container>()
         .Property(u => u.id)
         .HasDefaultValueSql("nextval('shared.\"t_container_seq\"')");

         modelBuilder.Entity<Container>()
         .Property(c => c.createtime)
         .HasDefaultValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

         modelBuilder.Entity<Container>()
         .Property(c => c.updatetime)
         .HasDefaultValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
         base.OnModelCreating(modelBuilder);
        }
    }
}
