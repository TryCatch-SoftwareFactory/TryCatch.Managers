// <copyright file="ISpecFactory{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.Specs
{
    using TryCatch.Models;
    using TryCatch.Patterns.Specifications;

    /// <summary>
    /// Specifications factory. Allows getting the specifications for entities queries from the repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity used on the repository.</typeparam>
    public interface ISpecFactory<TEntity>
    {
        /// <summary>
        /// Gets the default specification for lists or queries.
        /// </summary>
        /// <returns>Spec of default query.</returns>
        ISpecification<TEntity> GetDefaultSpec();

        /// <summary>
        /// Gets the spec for the read query.
        /// </summary>
        /// <param name="entity">Entity with data for query (p.e: identity value).</param>
        /// <returns>Spec for the query.</returns>
        ISpecification<TEntity> GetReadSpec(TEntity entity);

        /// <summary>
        /// Gets the spec for the delete query.
        /// </summary>
        /// <param name="entity">Entity with data for query (p.e: identity value).</param>
        /// <returns>Spec for the query.</returns>
        ISpecification<TEntity> GetDeleteSpec(TEntity entity);

        /// <summary>
        /// Gets the spec for the paged list query.
        /// </summary>
        /// <param name="pageFilter">Query data filter.</param>
        /// <returns>Spec for the query.</returns>
        ISpecification<TEntity> GetPageSpec(PageModel pageFilter);

        /// <summary>
        /// Gets the "sort as" spec for any list type query.
        /// </summary>
        /// <param name="pageFilter">Query data filter.</param>
        /// <returns>Sort spec for the query.</returns>
        ISortSpecification<TEntity> GetOrderSpec(PageModel pageFilter);
    }
}
