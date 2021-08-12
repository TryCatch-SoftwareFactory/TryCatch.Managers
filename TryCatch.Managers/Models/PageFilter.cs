// <copyright file="PageFilter.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Managers.Models
{
    /// <summary>
    /// Represents the common criteria for the pagination filter in queries.
    /// </summary>
    public class PageFilter
    {
        private string sortAs;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageFilter"/> class.
        /// </summary>
        public PageFilter()
        {
            this.sortAs = string.Empty;
        }

        /// <summary>
        /// Gets or sets the offset to be used in the query.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the size of page to be used in the query.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets the criteria for the filter.
        /// </summary>
        public string SearchCriteria { get; set; }

        /// <summary>
        /// Gets or sets the field to be use as sorts criteria.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Sets if the sort is ascending or descending.
        /// </summary>
        public string SortAs
        {
            set
            {
                this.sortAs = value;
            }
        }

        /// <summary>
        /// Gets a flag that indicates if must be sorted ascending. By default, the order is descending.
        /// </summary>
        /// <returns>True if must be ascending.</returns>
        public bool SortAsAscending()
        {
            if (string.IsNullOrWhiteSpace(this.sortAs))
            {
                return false;
            }

            return this.sortAs
                .ToUpperInvariant()
                .Equals(Constants.SortAsAscending, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
