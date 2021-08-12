// <copyright file="SortTrainSpec.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Mocks.Specs
{
    using System;
    using System.Linq.Expressions;
    using TryCatch.Patterns.Specifications;

    public class SortTrainSpec : ISortSpecification<Train>
    {
        private const string DefaultField = "Name";

        private readonly bool sortAsAscending;

        private readonly string fieldName;

        public SortTrainSpec(bool asAscending = false, string fieldName = DefaultField)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                fieldName = DefaultField;
            }

            this.sortAsAscending = asAscending;

            this.fieldName = fieldName.ToUpperInvariant();
        }

        public Expression<Func<Train, object>> AsExpression() =>
            this.fieldName switch
            {
                "ID" => (x) => x.Id,
                _ => (x) => x.Reference,
            };

        public bool IsAscending() => this.sortAsAscending;
    }
}
