using DLL.Models;
using DLL.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class ApplicationDbContext : DbContext
    {

        private static readonly MethodInfo _propertyMethod = typeof(EF).GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public)?.MakeGenericMethod(typeof(bool));
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        private static LambdaExpression GetIsDeletedRestiction(Type type)
        {
            var parm = Expression.Parameter(type, "it");
            var prop = Expression.Call(_propertyMethod, parm, Expression.Constant(IsDeleted));
            var condition=Expression.MakeBinary(ExpressionType.Equal,prop,Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parm);
            return lambda;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            //got get the deleted data
           foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                if(typeof(ISoftDelete).IsAssignableFrom(entity.ClrType)==true)
                {
                    entity.AddProperty(IsDeleted, typeof(bool));
                    modelBuilder.Entity(entity.ClrType).HasQueryFilter(GetIsDeletedRestiction(entity.ClrType));
                }
            }
        }

        public override int SaveChanges()
        {
            onBeforeSavingData();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            onBeforeSavingData();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void onBeforeSavingData()
        {

            var entries = ChangeTracker.Entries().Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged);
            foreach(var entry in entries)
            {
                if(entry is ITrackable trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            trackable.createdAt = DateTimeOffset.Now;
                            trackable.lastUpdated = DateTimeOffset.Now;
                            break;
                        case EntityState.Modified:
                            trackable.lastUpdated = DateTimeOffset.Now;
                            break;
                        case EntityState.Deleted:
                            entry.Property(IsDeleted).CurrentValue = true;
                            break;
                    }

                }
              
            }
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        private const string IsDeleted = "Isdeleted";
    }
}
