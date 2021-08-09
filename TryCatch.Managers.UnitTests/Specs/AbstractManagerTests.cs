// <copyright file="AbstractManagerTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Specs
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NSubstitute;
    using TryCatch.Managers.Models;
    using TryCatch.Managers.Specs;
    using TryCatch.Managers.UnitTests.Mocks;
    using TryCatch.Managers.UnitTests.Mocks.Specs;
    using TryCatch.Managers.Validators;
    using TryCatch.Patterns.Repositories;
    using TryCatch.Patterns.Results;
    using TryCatch.Patterns.Specifications;
    using Xunit;

    public class AbstractManagerTests
    {
        private readonly ISpecRepository<Train> repository;

        private readonly IEntityValidatorsFactory validatorsFactory;

        private readonly IResultBuilderFactory resultBuilderFactory;

        private readonly ISpecFactory<Train> specFactory;

        private readonly TrainsManager sut;

        public AbstractManagerTests()
        {
            this.repository = Substitute.For<ISpecRepository<Train>>();
            this.validatorsFactory = Substitute.For<IEntityValidatorsFactory>();
            this.resultBuilderFactory = Given.GetBuilder;
            this.specFactory = Substitute.For<ISpecFactory<Train>>();
            this.sut = new TrainsManager(
                this.repository,
                this.validatorsFactory,
                this.resultBuilderFactory,
                this.specFactory);
        }

        [Fact]
        public void Construct_without_repository()
        {
            // Arrange
            ISpecRepository<Train> repository = null;

            // Act
            Action act = () => _ = new TrainsManager(
                repository,
                this.validatorsFactory,
                this.resultBuilderFactory,
                this.specFactory);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_validatorsFactory()
        {
            // Arrange
            IEntityValidatorsFactory validatorsFactory = null;

            // Act
            Action act = () => _ = new TrainsManager(
                this.repository,
                validatorsFactory,
                this.resultBuilderFactory,
                this.specFactory);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_resultBuilderFactory()
        {
            // Arrange
            IResultBuilderFactory resultBuilderFactory = null;

            // Act
            Action act = () => _ = new TrainsManager(
                this.repository,
                this.validatorsFactory,
                resultBuilderFactory,
                this.specFactory);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_specFactory()
        {
            // Arrange
            ISpecFactory<Train> specFactory = null;

            // Act
            Action act = () => _ = new TrainsManager(
                this.repository,
                this.validatorsFactory,
                this.resultBuilderFactory,
                specFactory);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async void Create_without_Entity()
        {
            // Arrange
            Train entity = null;

            // Act
            Func<Task> act = async () => await this.sut.Create(entity).ConfigureAwait(false);

            // Asserts
            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Fact]
        public async void Create_with_repository_error()
        {
            // Arrange
            var entity = EntityFactory.Get<Train>();

            // Act
            var actual = await this.sut.Create(entity).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.IsSucceeded.Should().BeFalse();
            actual.Payload.Should().NotBeNull();
        }

        [Fact]
        public async void Create_Ok()
        {
            // Arrange
            var entity = EntityFactory.Get<Train>();

            this.repository
                .AddAsync(Arg.Any<Train>(), Arg.Any<CancellationToken>())
                .Returns(true);

            // Act
            var actual = await this.sut.Create(entity).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.IsSucceeded.Should().BeTrue();
            actual.Payload.Should().BeEquivalentTo(entity);

            await this.repository
                .Received(1)
                .AddAsync(Arg.Any<Train>(), Arg.Any<CancellationToken>())
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Read_without_Entity()
        {
            // Arrange
            Train entity = null;

            // Act
            Func<Task> act = async () => await this.sut.Read(entity).ConfigureAwait(false);

            // Asserts
            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Fact]
        public async void Read_with_not_found_entity()
        {
            // Arrange
            var entity = EntityFactory.Get<Train>();

            this.specFactory.GetReadSpec(Arg.Any<Train>()).Returns(new ReadTrainSpec("id"));

            this.repository
                .GetAsync(Arg.Any<ISpecification<Train>>(), Arg.Any<CancellationToken>())
                .Returns(default(Train));

            // Act
            var actual = await this.sut.Read(entity).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.IsSucceeded.Should().BeFalse();
            actual.Payload.Should().BeNull();

            await this.repository
                .Received(1)
                .GetAsync(Arg.Any<ISpecification<Train>>(), Arg.Any<CancellationToken>())
                .ConfigureAwait(false);

            this.specFactory
                .Received(1)
                .GetReadSpec(Arg.Any<Train>());
        }

        [Fact]
        public async void Read_Ok()
        {
            // Arrange
            var entity = EntityFactory.Get<Train>();

            this.specFactory.GetReadSpec(Arg.Any<Train>()).Returns(new ReadTrainSpec("id"));

            this.repository
                .GetAsync(Arg.Any<ISpecification<Train>>(), Arg.Any<CancellationToken>())
                .Returns(entity);

            // Act
            var actual = await this.sut.Read(entity).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.IsSucceeded.Should().BeTrue();
            actual.Payload.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public async void Update_without_Entity()
        {
            // Arrange
            Train entity = null;

            // Act
            Func<Task> act = async () => await this.sut.Update(entity).ConfigureAwait(false);

            // Asserts
            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Fact]
        public async void Update_with_repository_error()
        {
            // Arrange
            var entity = EntityFactory.Get<Train>();

            // Act
            var actual = await this.sut.Update(entity).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.IsSucceeded.Should().BeFalse();
        }

        [Fact]
        public async void Update_Ok()
        {
            // Arrange
            var entity = EntityFactory.Get<Train>();

            this.repository
                .UpdateAsync(Arg.Any<Train>(), Arg.Any<CancellationToken>())
                .Returns(true);

            // Act
            var actual = await this.sut.Update(entity).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.IsSucceeded.Should().BeTrue();

            await this.repository
                .Received(1)
                .UpdateAsync(Arg.Any<Train>(), Arg.Any<CancellationToken>())
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Delete_without_Entity()
        {
            // Arrange
            Train entity = null;

            // Act
            Func<Task> act = async () => await this.sut.Delete(entity).ConfigureAwait(false);

            // Asserts
            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Fact]
        public async void Delete_with_repository_error()
        {
            // Arrange
            var entity = EntityFactory.Get<Train>();

            // Act
            var actual = await this.sut.Delete(entity).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.IsSucceeded.Should().BeFalse();
        }

        [Fact]
        public async void Delete_Ok()
        {
            // Arrange
            var entity = EntityFactory.Get<Train>();

            this.repository
                .DeleteAsync(Arg.Any<ISpecification<Train>>(), Arg.Any<CancellationToken>())
                .Returns(true);

            // Act
            var actual = await this.sut.Delete(entity).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.IsSucceeded.Should().BeTrue();

            await this.repository
                .Received(1)
                .DeleteAsync(Arg.Any<ISpecification<Train>>(), Arg.Any<CancellationToken>())
                .ConfigureAwait(false);
        }

        [Fact]
        public async void GetPage_without_filter()
        {
            // Arrange
            PageFilter pageFilter = null;

            // Act
            Func<Task> act = async () => await this.sut.GetPage(pageFilter).ConfigureAwait(false);

            // Asserts
            await act.Should().ThrowAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [Fact]
        public async void GetPage_Ok()
        {
            // Arrange
            const long Total = 100L;
            const long Matched = 0L;

            var pageFilter = EntityFactory.Get<PageFilter>();
            var entities = EntityFactory.Get<Train>(10);

            this.specFactory.GetDefaultSpec().Returns(new ReadTrainSpec("id"));
            this.specFactory.GetPageSpec(Arg.Any<PageFilter>()).Returns(new ReadTrainSpec("id"));
            this.specFactory.GetOrderSpec(Arg.Any<PageFilter>()).Returns(new SortTrainSpec(true, "id"));
            this.repository.GetCountAsync(Arg.Any<ISpecification<Train>>()).Returns(Total);
            this.repository.GetCountAsync(Arg.Any<ISpecification<Train>>()).Returns(Matched);
            this.repository
                .GetPageAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<ISpecification<Train>>(), Arg.Any<ISortSpecification<Train>>())
                .Returns(entities);

            // Act
            var actual = await this.sut.GetPage(pageFilter).ConfigureAwait(false);

            // Asserts
            actual.Should().NotBeNull();
            actual.Items.Should().BeEquivalentTo(entities);

            this.specFactory.Received(1).GetDefaultSpec();
            this.specFactory.Received(1).GetPageSpec(Arg.Any<PageFilter>());
            this.specFactory.Received(1).GetOrderSpec(Arg.Any<PageFilter>());

            await this.repository
                .Received(2)
                .GetCountAsync(Arg.Any<ISpecification<Train>>())
                .ConfigureAwait(false);

            await this.repository
                .Received(1)
                .GetPageAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<ISpecification<Train>>(), Arg.Any<ISortSpecification<Train>>())
                .ConfigureAwait(false);
        }
    }
}
