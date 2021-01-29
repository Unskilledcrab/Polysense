using Microsoft.EntityFrameworkCore;
using PS.Shared.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PS.Web.API.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task BulkUpdateOrCreateAsync<T>(this DbContext dbContext, DbSet<T> dbSet, IEnumerable<T> model) where T : BaseEntity
        {
            var dbModel = await dbSet.FindAsync(model.Id);
            if (dbModel == null)
            {
                dbSet.AddRange(model);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                dbContext.Entry(dbModel).State = EntityState.Detached;
                model.Id = dbModel.Id;
                dbContext.Entry(model).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task UpdateOrCreateAsync<T>(this DbContext dbContext, DbSet<T> dbSet, T model) where T : BaseEntity
        {
            if (model.Id <= 0)
            {
                dbSet.Add(model);
                await dbContext.SaveChangesAsync();
                return;
            }
            var dbModel = await dbSet.FindAsync(model.Id);
            if (dbModel == null)
            {
                dbSet.Add(model);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                dbContext.Entry(dbModel).State = EntityState.Detached;
                model.Id = dbModel.Id;
                dbContext.Entry(model).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task UpdateOrCreateAsync<T>(this DbContext dbContext, DbSet<T> dbSet, T model, Expression<Func<T, bool>> identityPredicate) where T : BaseEntity
        {
            var dbModel = await dbSet.AsNoTracking().FirstOrDefaultAsync(identityPredicate);
            if (dbModel == null)
            {
                dbSet.Add(model);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                model.Id = dbModel.Id;
                dbContext.Entry(model).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task<bool> TryDeleteAsync<T>(this DbContext dbContext, DbSet<T> dbSet, int id) where T : class
        {
            var dbModel = await dbSet.FindAsync(id);
            if (dbModel != null)
            {
                dbSet.Remove(dbModel);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public static async Task<bool> TryDeleteAsync<T>(this DbContext dbContext, DbSet<T> dbSet, T model) where T : BaseEntity
        {
            var dbModel = await dbSet.FindAsync(model.Id);
            if (dbModel != null)
            {
                dbSet.Remove(dbModel);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public static async Task<bool> TryDeleteAsync<T>(this DbContext dbContext, DbSet<T> dbSet, Expression<Func<T, bool>> identityPredicate) where T : BaseEntity
        {
            var dbModel = await dbSet.FirstOrDefaultAsync(identityPredicate);
            if (dbModel != null)
            {
                dbSet.Remove(dbModel);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}