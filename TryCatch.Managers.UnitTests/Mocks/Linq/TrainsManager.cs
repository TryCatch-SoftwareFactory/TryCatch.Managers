// <copyright file="TrainsManager.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Mocks.Linq
{
    using TryCatch.Managers.Linq;
    using TryCatch.Managers.Validators;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;

    public class TrainsManager : AbstractManager<Train>
    {
        public TrainsManager(
            ILinqRepository<Train> repository,
            IEntityValidatorsFactory validatorsFactory,
            IResultBuilderFactory resultBuilderFactory,
            IExpressionFactory<Train> expressionFactory)
            : base(repository, validatorsFactory, resultBuilderFactory, expressionFactory)
        {
        }
    }
}
