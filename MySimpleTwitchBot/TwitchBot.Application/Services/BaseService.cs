using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Application.Interfaces;
using TwitchBot.Domain.Interfaces;
using TwitchBot.Domain.Repositories;

namespace TwitchBot.Application.Services
{
    /// <summary>
    /// Service base responsible for handling all services that needs logic inplementation.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseService<T> : IBaseService<T>
        where T : class, IEntity
    {
        /// <summary>
        /// Protected access to repository.
        /// </summary>
        /// <value></value>
        protected readonly IRepositoryBase<T> repository;

        /// <summary>
        /// Getting repository through constructor and DI.
        /// </summary>
        /// <param name="repository"></param>
        protected BaseService(IRepositoryBase<T> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Add an entity to the database.
        /// </summary>
        /// <param name="entity">Valid entity</param>
        /// <returns></returns>
        public virtual async Task Add(T entity)
        {
            try
            {
                await this.repository.Add(entity);
            }
            catch { throw; }
        }

        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        /// <exception cref="OverflowException">The number of elements in source is larger than <see cref="Int32.MaxValue" />.</exception>
        /// <returns><see cref="Int32" /> The number of elements in the input sequence.</returns>
        public virtual async Task<int> Count()
        {
            try
            {
                return await this.repository.Count();
            } catch { throw; }
        }

        public virtual async Task<T> GetById(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                return await this.repository.GetById(guid);
            }
            catch { throw; }
        }

        public Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task Remove(string Id)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
