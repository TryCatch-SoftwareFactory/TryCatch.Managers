// <copyright file="TrainsManager.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Mocks.Specs
{
    using TryCatch.Managers.Specs;
    using TryCatch.Managers.Validators;
    using TryCatch.Patterns.Repositories.Spec;
    using TryCatch.Patterns.Results;

    public class TrainsManager : AbstractManager<Train>
    {
        public TrainsManager(
            IRepository<Train> repository,
            IEntityValidatorsFactory validatorsFactory,
            IResultBuilderFactory resultBuilderFactory,
            ISpecFactory<Train> specFactory)
            : base(repository, validatorsFactory, resultBuilderFactory, specFactory)
        {
        }
    }
}
