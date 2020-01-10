using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TwitchBot.Data.Context;
using TwitchBot.Domain.Interfaces;
using TwitchBot.Domain.Repositories;

namespace TwitchBot.Data.Repositories
{
    /// <summary>
    /// Serves as a base repository for all database models
    /// </summary>
    /// <typeparam name="T">An existing model/class</typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        /// <summary>
        /// Protected access to the context
        /// </summary>
        protected readonly TwitchBotContext _context;

        /// <summary>
        /// Protected constructor for the base repository using a valid context
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected RepositoryBase(TwitchBotContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Cannot initialiaze a repository with a null context");
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Returns a <see cref="IEnumerable{T}"/></returns>
        public virtual async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();

        /// <summary>
        /// Returns the first entity that satisfies the condition or default value if no such is found.
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{T,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="T"/></returns>
        public virtual async Task<T> FirstOrDefault(Expression<Func<T, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where), "Predicate cannot be null.");
            try
            {
                return await _context.Set<T>().FirstOrDefaultAsync(where);
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
            catch (ArgumentException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="where">A valid <see cref="Expression{Func{T,bool}}" /> predicate.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="IEnumerable{T}"/></returns>
        public virtual async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where), "Predicate cannot be null.");

            try
            {
                return await _context.Set<T>().Where(where).ToListAsync();
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
            catch (ArgumentException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
        }

        /// <summary>
        /// Returns the number of elements in a sequence that satisfy a condition.
        /// </summary>
        /// <param name="where">A valid <see cref="Expression{Func{T,bool}}" /> predicate.</param>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        /// <exception cref="OverflowException">The number of elements in source is larger than <see cref="Int32.MaxValue" />.</exception>
        /// <returns><see cref="Int32" /> The number of elements in the input sequence.</returns>
        public async Task<int> Count(Expression<Func<T, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(
                    message: "Predicate cannot be null.",
                    paramName: nameof(where));

            try
            {
                return await _context.Set<T>().CountAsync(where);
            }
            catch (ArgumentNullException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
            catch (OverflowException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
        }

        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        /// <exception cref="OverflowException">The number of elements in source is larger than <see cref="Int32.MaxValue" />.</exception>
        /// <returns><see cref="Int32" /> The number of elements in the input sequence.</returns>
        public async Task<int> Count()
        {
            try
            {
                return await _context.Set<T>().CountAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (OverflowException)
            {
                throw;
            }
        }

        /// <summary>
        /// Search for a entity based on an id.
        /// </summary>
        /// <param name="id">(integer)</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception" />
        /// <returns>Returns a <see cref="T"/></returns>
        public virtual async Task<T> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id cannot be null", nameof(id));

            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> { id };
                throw;
            }
        }

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="model"><see cref="T"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="bool"/>Returns the model</returns>
        public virtual async Task Add(T model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot add a null reference");

            try
            {
                await _context.Set<T>().AddAsync(model);
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
        }

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
        public virtual async Task Update(T model)
        {
            if (model == null)
                throw new ArgumentNullException(
                    message: "Cannot update a null reference",
                    paramName: nameof(model));

            try
            {
                _context.Set<T>().Update(model);
                await _context.SaveChangesAsync();
            }
            catch (EntryPointNotFoundException ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
            catch (DbUpdateException ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
        }

        /// <summary>
        /// Remove the entity
        /// </summary>
        /// <param name="model">A valid <see cref="T"/> model.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="bool"/> true if removed, false if not</returns>
        public virtual async Task<bool> Remove(T model)
        {
            if (model == null)
                throw new ArgumentNullException(
                    message: "Cannot remove a null object",
                    paramName: nameof(model));

            try
            {
                _context.Set<T>().Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
