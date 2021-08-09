// <copyright file="EntityFactory.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.UnitTests.Mocks
{
    using System.Collections.Generic;
    using AutoFixture;

    public static class EntityFactory
    {
        public static T Get<T>()
            where T : class
        {
            var fixture = new Fixture();

            return fixture.Build<T>().Create();
        }

        public static IEnumerable<T> Get<T>(int length)
            where T : class
        {
            var fixture = new Fixture();
            var list = new HashSet<T>();

            for (var i = 0; i < length; i++)
            {
                var item = fixture.Build<T>().Create();

                list.Add(item);
            }

            return list;
        }
    }
}
