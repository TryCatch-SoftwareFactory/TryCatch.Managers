// <copyright file="IEntityManager{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers
{
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Managers.Models;
    using TryCatch.Patterns.Results;

    /// <summary>
    /// Manager interface. Allows managing the most common operations over an entity and their children.
    /// </summary>
    /// <typeparam name="TEntity">Type of root entity.</typeparam>
    public interface IEntityManager<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Allows creating a new entity on the system.
        /// </summary>
        /// <param name="entity">Entity data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information on the result of the operation.</returns>
        Task<Result<TEntity>> Create(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Allows deleting an existing entity.
        /// </summary>
        /// <param name="entity">Entity data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information on the result of the operation.</returns>
        Task<OpResult> Delete(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Allows getting a page of entities matched with the filter criteria.
        /// </summary>
        /// <param name="pageFilter">the paging filter.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Page of results.</returns>
        Task<PageResult<TEntity>> GetPage(PageFilter pageFilter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Allows reading an existing entity.
        /// </summary>
        /// <param name="entity">Entity data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information on the result of the operation.</returns>
        Task<Result<TEntity>> Read(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Allows updating an existing entity.
        /// </summary>
        /// <param name="entity">Entity data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Information on the result of the operation.</returns>
        Task<OpResult> Update(TEntity entity, CancellationToken cancellationToken = default);
    }
}