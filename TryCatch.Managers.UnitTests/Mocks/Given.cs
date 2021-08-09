// <copyright file="Given.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Mocks
{
    using TryCatch.Patterns.Results;

    public static class Given
    {
        public static IResultBuilderFactory GetBuilder => new BuilderFactory();
    }
}
