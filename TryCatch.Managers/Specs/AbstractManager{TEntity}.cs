// <copyright file="AbstractManager{TEntity}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.Specs
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using TryCatch.Managers.Models;
    using TryCatch.Managers.Validators;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Validators;

    /// <summary>
    /// Abstract base class for building entity managers with the most common operations.
    /// </summary>
    /// <typeparam name="TEntity">Type of root entity.</typeparam>
    public abstract class AbstractManager<TEntity> : IEntityManager<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractManager{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">Specification repository reference.</param>
        /// <param name="validatorsFactory">Validators factory reference.</param>
        /// <param name="resultBuilderFactory">Result builder factory reference.</param>
        /// <param name="specFactory">Specification factory reference.</param>
        protected AbstractManager(
            ISpecRepository<TEntity> repository,
            IEntityValidatorsFactory validatorsFactory,
            IResultBuilderFactory resultBuilderFactory,
            ISpecFactory<TEntity> specFactory)
        {
            this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.ValidatorsFactory = validatorsFactory ?? throw new ArgumentNullException(nameof(validatorsFactory));
            this.ResultBuilderFactory = resultBuilderFactory ?? throw new ArgumentNullException(nameof(resultBuilderFactory));
            this.SpecFactory = specFactory ?? throw new ArgumentNullException(nameof(specFactory));
        }

        /// <summary>
        /// Gets the repository reference.
        /// </summary>
        protected ISpecRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the validator factory reference.
        /// </summary>
        protected IEntityValidatorsFactory ValidatorsFactory { get; }

        /// <summary>
        /// Gets the result builder factory reference.
        /// </summary>
        protected IResultBuilderFactory ResultBuilderFactory { get; }

        /// <summary>
        /// Gets the specification factory reference.
        /// </summary>
        protected ISpecFactory<TEntity> SpecFactory { get; }

        /// <inheritdoc/>
        public async virtual Task<Result<TEntity>> Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(entity);

            await this.ValidatorsFactory
                .GetCreateValidator()
                .ValidateAndThrowIfErrorAsync(entity, cancellationToken)
                .ConfigureAwait(false);

            var result = await this.Repository
                .AddAsync(entity, cancellationToken)
                .ConfigureAwait(false);

            var message = result ? string.Empty : "Something was wrong with the creation";

            return this.ResultBuilderFactory
                .GetPayloadResultBuilder<TEntity>()
                .Build()
                .WithPayload(entity)
                .WithError(message)
                .Create();
        }

        /// <inheritdoc/>
        public async virtual Task<Result<TEntity>> Read(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(entity);

            var spec = this.SpecFactory.GetReadSpec(entity);

            var foundedEntity = await this.Repository
                .GetAsync(spec, cancellationToken)
                .ConfigureAwait(false);

            var message = foundedEntity is null ? "Entity not found!" : string.Empty;

            var builder = this.ResultBuilderFactory
                .GetPayloadResultBuilder<TEntity>()
                .Build()
                .WithError(message);

            return foundedEntity is default(TEntity)
                ? builder.Create()
                : builder.WithPayload(foundedEntity).Create();
        }

        /// <inheritdoc/>
        public async virtual Task<OpResult> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(entity);

            await this.ValidatorsFactory
                .GetUpdateValidator()
                .ValidateAsync(entity, cancellationToken)
                .ConfigureAwait(false);

            var result = await this.Repository
                .UpdateAsync(entity, cancellationToken)
                .ConfigureAwait(false);

            var message = result ? string.Empty : "Something was wrong with the update!";

            return this.ResultBuilderFactory
                .GetOperationResultBuilder()
                .Build()
                .WithError(message)
                .Create();
        }

        /// <inheritdoc/>
        public async virtual Task<OpResult> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(entity);

            var spec = this.SpecFactory.GetDeleteSpec(entity);

            var result = await this.Repository
                .DeleteAsync(spec, cancellationToken)
                .ConfigureAwait(false);

            var message = result ? string.Empty : "Something was wrong with the delete!";

            return this.ResultBuilderFactory
                .GetOperationResultBuilder()
                .Build()
                .WithError(message)
                .Create();
        }

        /// <inheritdoc/>
        public async virtual Task<PageResult<TEntity>> GetPage(PageFilter pageFilter, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ArgumentsValidator.ThrowIfIsNull(pageFilter);

            await this.ValidatorsFactory
                .GetPageValidator()
                .ValidateAndThrowIfErrorAsync(pageFilter, cancellationToken)
                .ConfigureAwait(false);

            var defaultSpec = this.SpecFactory.GetDefaultSpec();
            var spec = this.SpecFactory.GetPageSpec(pageFilter);
            var orderBy = this.SpecFactory.GetOrderSpec(pageFilter);

            var countTask = this.Repository.GetCountAsync(defaultSpec, cancellationToken);
            var matchedTask = this.Repository.GetCountAsync(spec, cancellationToken);
            var itemsTask = this.Repository.GetPageAsync(pageFilter.Offset, pageFilter.Limit, spec, orderBy, cancellationToken);

            var tasks = new Task[] { countTask, matchedTask, itemsTask };

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return this.ResultBuilderFactory
                .GetPageResultBuilder<TEntity>()
                .Build()
                .WithCount(await countTask.ConfigureAwait(false))
                .WithMatched(await matchedTask.ConfigureAwait(false))
                .WithItems(await itemsTask.ConfigureAwait(false))
                .WithOffset(pageFilter.Offset)
                .WithLimit(pageFilter.Limit)
                .Create();
        }
    }
}
