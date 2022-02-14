// <copyright file="ListTrainsSpec.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Mocks.Specs
{
    using System;
    using System.Linq.Expressions;
    using TryCatch.Patterns.Specifications.Linq;

    public class ListTrainsSpec : CompositeSpecification<Train>, ILinqSpecification<Train>
    {
        private readonly string nameCriteria;

        public ListTrainsSpec(string nameCriteria)
        {
            if (string.IsNullOrWhiteSpace(nameCriteria))
            {
                nameCriteria = string.Empty;
            }

            this.nameCriteria = nameCriteria;
        }

        public override Expression<Func<Train, bool>> AsExpression() => (x) => x.Reference.Contains(this.nameCriteria);
    }
}
