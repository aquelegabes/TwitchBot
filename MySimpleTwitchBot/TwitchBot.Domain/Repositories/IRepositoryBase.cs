using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TwitchBot.Domain.Interfaces;

namespace TwitchBot.Domain.Repositories
{
    /// <summary>
    /// Interface to serve as a base repository for all database models
    /// </summary>
    /// <typeparam name="T">An existing model/class <see cref="IEntity"/>.</typeparam>
    public interface IRepositoryBase<T> : IDisposable
        where T : class, IEntity
    {
        /// <summary>   
        /// Search for a entity based on an id.
        /// </summary>
        /// <param name="id"><see cref="Guid"></see></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>Returns a <see cref="T"/></returns>
        Task<T> GetById(Guid id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Returns a <see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Returns the first entity that satisfies the condition or default value if no such is found.
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{T,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="T"/></returns>
        Task<T> FirstOrDefault(Expression<Func<T, bool>> @where);

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="where">A valid <see cref="Expression{Func{T,bool}}" /> predicate.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> @where);

        /// <summary>
        /// Returns the number of elements in a sequence that satisfy a condition.
        /// </summary>
        /// <param name="where">A valid <see cref="Expression{Func{T,bool}}" /> predicate.</param>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        /// <exception cref="OverflowException">The number of elements in source is larger than <see cref="Int32.MaxValue" />.</exception>
        /// <returns><see cref="Int32" /> The number of elements in the input sequence.</returns>
        Task<int> Count(Expression<Func<T, bool>> @where);

        /// <summary>
        /// Returns the number of elements in collection.
        /// </summary>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        /// <exception cref="OverflowException">The number of elements in source is larger than <see cref="Int32.MaxValue" />.</exception>
        /// <returns><see cref="Int32" /> The number of elements in the input sequence.</returns>
        Task<int> Count();

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="model"><see cref="T"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns the added <see cref="T"/> model.</returns>
        Task<T> Add(T model);

        /// <summary>
        /// Update the entity.
        /// </summary>
        /// <param name="model"><see cref="T"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EntryPointNotFoundException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns the updated <see cref="T"/> entity.</returns>
        Task<T> Update(T model);

        /// <summary>
        /// Remove the entity
        /// </summary>
        /// <param name="model">A valid <see cref="T"/> model.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="bool"/> true if removed, false if not</returns>
        Task<bool> Remove(T model);
    }
}
