// <copyright file="IExpressionFactory{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.Linq
{
    using System;
    using System.Linq.Expressions;
    using TryCatch.Managers.Models;

    /// <summary>
    /// Specifications factory. Allows getting the linq specifications for entities queries from the repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity used on the repository.</typeparam>
    public interface IExpressionFactory<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets the default linq specification for lists or queries.
        /// </summary>
        /// <returns>Spec of default query.</returns>
        Expression<Func<TEntity, bool>> GetDefaultSpec();

        /// <summary>
        /// Gets the linq spec for the read query.
        /// </summary>
        /// <param name="entity">Entity with data for query (p.e: identity value).</param>
        /// <returns>Spec for the query.</returns>
        Expression<Func<TEntity, bool>> GetReadSpec(TEntity entity);

        /// <summary>
        /// Gets the linq spec for the paged list query.
        /// </summary>
        /// <param name="pageFilter">Query data filter.</param>
        /// <returns>Spec for the query.</returns>
        Expression<Func<TEntity, bool>> GetPageSpec(PageFilter pageFilter);

        /// <summary>
        /// Gets the "sort as" linq spec for any list type query.
        /// </summary>
        /// <param name="pageFilter">Query data filter.</param>
        /// <returns>Sort spec for the query.</returns>
        Expression<Func<TEntity, object>> GetOrderSpec(PageFilter pageFilter);
    }
}
