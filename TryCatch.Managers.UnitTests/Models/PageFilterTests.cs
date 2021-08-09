// <copyright file="PageFilterTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Models
{
    using FluentAssertions;
    using TryCatch.Managers.Models;
    using Xunit;

    public class PageFilterTests
    {
        [Fact]
        public void Filter_as_ascending()
        {
            // Arrange
            var filter = new PageFilter()
            {
                SortAs = Constants.SortAsAscending,
            };

            // Act
            var actual = filter.SortAsAscending();

            // Asserts
            actual.Should().BeTrue();
        }

        [Fact]
        public void Filter_as_descending()
        {
            // Arrange
            var filter = new PageFilter()
            {
                SortAs = Constants.SortAsDescending,
            };

            // Act
            var actual = filter.SortAsAscending();

            // Asserts
            actual.Should().BeFalse();
        }

        [Theory]
        [InlineData("any-value")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Filter_default_as_descending(string sortAs)
        {
            // Arrange
            var filter = new PageFilter()
            {
                SortAs = sortAs,
            };

            // Act
            var actual = filter.SortAsAscending();

            // Asserts
            actual.Should().BeFalse();
        }
    }
}
