// <copyright file="ReadTrainSpec.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Mocks.Specs
{
    using System;
    using System.Linq.Expressions;
    using TryCatch.Patterns.Specifications.Linq;
    using TryCatch.Validators;

    public class ReadTrainSpec : CompositeSpecification<Train>, ILinqSpecification<Train>
    {
        private readonly string name;

        public ReadTrainSpec(string name)
        {
            ArgumentsValidator.ThrowIfIsNullEmptyOrWhiteSpace(name);

            this.name = name;
        }

        public override Expression<Func<Train, bool>> AsExpression() => (x) => x.Reference == this.name;
    }
}
