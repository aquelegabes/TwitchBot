using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TwitchBot.Domain.Interfaces;

namespace TwitchBot.Application.Interfaces
{
    /// <summary>
    /// Interface that serves as base logic for services.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseService<T>
        where T : class
    {
        /// <summary>
        /// Search for a entity based on an id.
        /// </summary>
        /// <param name="id">(integer)</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception" />
        /// <returns>Returns a <see cref="T"/></returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        /// <exception cref="OverflowException">The number of elements in source is larger than <see cref="Int32.MaxValue" />.</exception>
        /// <returns><see cref="Int32" /> The number of elements in the input sequence.</returns>
        Task<int> Count();

        /// <summary>
        /// Retrieve all entities within the expression.
        /// </summary>
        /// <param name="@where">A valid expression.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Null expression.</exception>
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> @where);

        /// <summary>
        /// Retrieve first entity that matches the expression.
        /// </summary>
        /// <param name="@where">A valid expression.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Null expression.</exception>
        Task<T> GetFirst(Expression<Func<T, bool>> where);

        /// <summary>
        /// Add an entity to the database.
        /// </summary>
        /// <param name="entity">Valid entity</param>
        /// <returns></returns>
        Task Add(T entity);

        /// <summary>
        /// Remove an entity.
        /// </summary>
        /// <param name="id">A valid entity id.</param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        Task Remove(string Id);

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="entity">Valid entity</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Null entity.</exception>
        /// <exception cref="KeyNotFound">Entity not found.</exception>
        Task Update(T entity);
    }
}
