using System.Collections.Generic;

namespace it.example.dotnetcore5.webapi.Models
{
    /// <summary>
    /// Represent a list result from web api
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T> : ApiResult
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// How many for page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of result
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// Paginated list of result
        /// </summary>
        public List<T> Items { get; set; }
    }
}
