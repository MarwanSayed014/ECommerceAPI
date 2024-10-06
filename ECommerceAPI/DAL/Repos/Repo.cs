using ECommerceAPI.BL.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerceAPI.DAL.Repos
{
    public class Repo<T> : IRepo<T> where T : class
    {
        protected readonly IDesignTimeDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public Repo(IDesignTimeDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }


        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                if(entity == null) 
                    throw new NullReferenceException("Entity should not be null");

                if (await FindWithKeysAsync(entity) != null)
                    throw new Exception("Entity already exists");

                using (ApplicationDbContext db = _dbContextFactory.CreateDbContext(null))
                {
                    await db.Set<T>().AddAsync(entity);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new NullReferenceException("Entity should not be null");
                if (await FindWithKeysAsync(entity) == null)
                    throw new Exception("Entity not exists");
                using (ApplicationDbContext db = _dbContextFactory.CreateDbContext(null))
                {
                    db.Entry(entity).State= EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new NullReferenceException("Entity should not be null");
                using (ApplicationDbContext db = _dbContextFactory.CreateDbContext(null))
                {
                    db.Set<T>().Remove(entity);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                if (expression == null)
                    throw new NullReferenceException("Expression should not be null");
                using (ApplicationDbContext db = _dbContextFactory.CreateDbContext(null))
                {
                    return db.Set<T>().Where(expression).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            try
            {
                using (ApplicationDbContext db = _dbContextFactory.CreateDbContext(null))
                {
                    return db.Set<T>().ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<T> FindWithKeysAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new NullReferenceException("Entity should not be null");
                var keysValues = GetKeysValues(entity);
                if (keysValues.Length == 0 || keysValues == null)
                    throw new NullReferenceException("Entity has not any Keys");
                using (ApplicationDbContext db = _dbContextFactory.CreateDbContext(null))
                {
                    return await db.Set<T>().FindAsync(GetKeysValues(entity));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IEnumerable<PropertyInfo> GetKeysProperties(T entity)
        {
            return entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(false)
                     .Any(a => a.GetType() == typeof(KeyAttribute)));
        }
        private object[] GetKeysValues(T entity)
        {
            var keyProperties = GetKeysProperties(entity);
            object[] keyValues = new object[keyProperties.Count()];
            int i = 0;
            foreach (var key in keyProperties)
            {
                keyValues[i] = key.GetValue(entity);
                i++;
            }
            return keyValues;
        }
    }
}
