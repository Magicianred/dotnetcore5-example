using it.example.dotnetcore5.domain.ModelsHelpers.Base;
using System;

namespace it.example.dotnetcore5.domain.ModelsHelpers
{
    /// <summary>
    /// Parameters for Post request
    /// </summary>
    public class PostParamsHelper : ListParamsHelper
    {
        /// <summary>
        /// Filter for post id
        /// </summary>
        public long? PostId { get; set; }
        /// <summary>
        /// Comparator for filter post id
        /// </summary>
        public string PostIdComparator { get; set; }

        /// <summary>
        /// Filter for post title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Comporator for filter post title
        /// </summary>
        public string TitleComparator { get; set; }

        /// <summary>
        /// Filter for post text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Compartor for filter post text
        /// </summary>
        public string TextComparator { get; set; }

        /// <summary>
        /// Filter for post create date
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// Comparator for filter post create date
        /// </summary>
        public string CreateDateComparator { get; set; }
    }
}
