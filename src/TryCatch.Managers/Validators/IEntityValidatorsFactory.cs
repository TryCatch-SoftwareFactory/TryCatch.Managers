// <copyright file="IEntityValidatorsFactory.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.Validators
{
    using TryCatch.Validators;

    /// <summary>
    /// Validators factory. Allows getting the validators for the business processes.
    /// </summary>
    public interface IEntityValidatorsFactory
    {
        /// <summary>
        /// Gets the validator for the create operation.
        /// </summary>
        /// <returns>Create validator.</returns>
        IValidator GetCreateValidator();

        /// <summary>
        /// Gets the validator for the update operation.
        /// </summary>
        /// <returns>Update validator.</returns>
        IValidator GetUpdateValidator();

        /// <summary>
        /// Gets the validator for the paging filter.
        /// </summary>
        /// <returns>Paging filter validator.</returns>
        IValidator GetPageValidator();
    }
}
