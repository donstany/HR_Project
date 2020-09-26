using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOWebFramework.Infrastructure.Data.Common
{
    /// <summary>
    /// Implementation of repository access methods
    /// for Relational Database Engine
    /// </summary>
    /// <typeparam name="T">Type of the data table to which 
    /// current reposity is attached</typeparam>
    public class Repository : IRepository
    {
        /// <summary>
        /// Entity framework DB context holding connection information and properties
        /// and tracking entity states 
        /// </summary>
        protected DbContext Context { get; set; }

        /// <summary>
        /// Representation of table in database
        /// </summary>
        protected DbSet<T> DbSet<T>() where T : class
        {

            return this.Context.Set<T>();

        }

        /// <summary>
        /// Public constructor to inject dependancies into the repository
        /// </summary>
        /// <param name="context">EF context to inject</param>
        public Repository(ApplicationDbContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Adds entity to the database
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public void Add<T>(T entity) where T : class
        {
            this.DbSet<T>().Add(entity);
        }

        /// <summary>
        /// Ads collection of entities to the database
        /// </summary>
        /// <param name="entities">Enumerable list of entities</param>
        public void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            this.DbSet<T>().AddRange(entities);
        }

        /// <summary>
        /// All records in a table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        public IQueryable<T> All<T>() where T : class
        {
            return this.DbSet<T>().AsQueryable();
        }

        public IQueryable<T> All<T>(Func<T, bool> search) where T : class
        {
            return this.DbSet<T>().Where(search).AsQueryable();
        }

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>Expression tree</returns>
        public IQueryable<T> AllReadonly<T>() where T : class
        {
            return this.DbSet<T>()
                .AsQueryable()
                .AsNoTracking();
        }
        public IQueryable<T> AllReadonly<T>(Func<T, bool> search) where T : class
        {
            return this.DbSet<T>()
                .Where(search)
                .AsQueryable()
                .AsNoTracking();
        }

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="id">Identificator of record to be deleted</param>
        public void Delete<T>(object id) where T : class
        {
            T entity = GetById<T>(id);

            Delete<T>(entity);
        }

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="entity">Entity representing record to be deleted</param>
        public void Delete<T>(T entity) where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet<T>().Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        /// <summary>
        /// Detaches given entity from the context
        /// </summary>
        /// <param name="entity">Entity to be detached</param>
        public void Detach<T>(T entity) where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public void Attach<T>(T entity) where T : class
        {
            var entry = this.Context.Attach(entity);
            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Disposing the context when it is not neede
        /// Don't have to call this method explicitely
        /// Leave it to the IoC container
        /// </summary>
        public void Dispose()
        {
            this.Context.Dispose();
        }

        /// <summary>
        /// Gets specific record from database by primary key
        /// </summary>
        /// <param name="id">record identificator</param>
        /// <returns>Single record</returns>
        public T GetById<T>(object id) where T : class
        {
            return this.DbSet<T>().Find(id);
        }

        /// <summary>
        /// Saves all made changes in trasaction
        /// </summary>
        /// <returns>Error code</returns>
        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        /// <summary>
        /// Updates a record in database
        /// </summary>
        /// <param name="entity">Entity for record to be updated</param>
        public void Update<T>(T entity) where T : class
        {
            this.DbSet<T>().Update(entity);
        }

        /// <summary>
        /// Updates set of records in the database
        /// </summary>
        /// <param name="entities">Enumerable collection of entities to be updated</param>
        public void UpdateRange<T>(IEnumerable<T> entities) where T : class
        {
            this.DbSet<T>().UpdateRange(entities);
        }

        /// <summary>
        /// Deletes multiple records 
        /// </summary>
        /// <param name="entities">Records to delete</param>
        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            this.DbSet<T>().RemoveRange(entities);
        }

        /// <summary>
        /// Deletes multiple records
        /// </summary>
        /// <param name="deleteWhereClause">Where clause</param>
        public void DeleteRange<T>(Func<T, bool> deleteWhereClause) where T : class
        {
            var entities = All<T>(deleteWhereClause);
            DeleteRange(entities);
        }


    }
}
