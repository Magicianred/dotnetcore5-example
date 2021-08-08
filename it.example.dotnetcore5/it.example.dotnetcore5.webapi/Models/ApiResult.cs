namespace it.example.dotnetcore5.webapi.Models
{
    /// <summary>
    /// Represent a result from web api
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// True if success, False if error
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// If error, there is a error message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
