// <copyright file="BuilderFactory.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Mocks
{
    using TryCatch.Patterns.Results;

    public class BuilderFactory : IResultBuilderFactory
    {
        public IOpResultBuilder GetOperationResultBuilder() => new OpResultBuilder();

        public IPageResultBuilder<TEntity> GetPageResultBuilder<TEntity>() => new PageResultBuilder<TEntity>();

        public IResultBuilder<TPayload> GetPayloadResultBuilder<TPayload>() => new ResultBuilder<TPayload>();
    }
}
