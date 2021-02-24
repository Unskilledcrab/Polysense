using Microsoft.EntityFrameworkCore;
using PS.Shared.Models.Abstractions;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PS.Web.API.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task<T> UpdateOrCreateAsync<T>(this DbContext dbContext, DbSet<T> dbSet, T model) where T : BaseEntity
        {
            if (model == null) return null;
            if (model.Id <= 0)
            {
                dbSet.Add(model);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
                return model;
            }
            var dbModel = await dbSet.FindAsync(model.Id).ConfigureAwait(false);
            if (dbModel == null)
            {
                dbSet.Add(model);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                dbContext.Entry(dbModel).State = EntityState.Detached;
                model.Id = dbModel.Id;
                dbContext.Entry(model).State = EntityState.Modified;
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            return model;
        }

        public static async Task<T> UpdateOrCreateAsync<T>(this DbContext dbContext, DbSet<T> dbSet, T model, Expression<Func<T, bool>> identityPredicate) where T : BaseEntity
        {
            if (model == null) return null;
            var dbModel = await dbSet.AsNoTracking().FirstOrDefaultAsync(identityPredicate).ConfigureAwait(false);
            if (dbModel == null)
            {
                dbSet.Add(model);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                model.Id = dbModel.Id;
                dbContext.Entry(model).State = EntityState.Modified;
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            return model;
        }

        public static async Task<bool> TryDeleteAsync<T>(this DbContext dbContext, DbSet<T> dbSet, int id) where T : class
        {
            var dbModel = await dbSet.FindAsync(id).ConfigureAwait(false);
            if (dbModel != null)
            {
                dbSet.Remove(dbModel);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            else
                return false;
        }

        public static async Task<bool> TryDeleteAsync<T>(this DbContext dbContext, DbSet<T> dbSet, T model) where T : BaseEntity
        {
            var dbModel = await dbSet.FindAsync(model.Id).ConfigureAwait(false);
            if (dbModel != null)
            {
                dbSet.Remove(dbModel);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            else
                return false;
        }

        public static async Task<bool> TryDeleteAsync<T>(this DbContext dbContext, DbSet<T> dbSet, Expression<Func<T, bool>> identityPredicate) where T : BaseEntity
        {
            var dbModel = await dbSet.FirstOrDefaultAsync(identityPredicate).ConfigureAwait(false);
            if (dbModel != null)
            {
                dbSet.Remove(dbModel);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            else
                return false;
        }
    }
}