namespace Task_Tracker.Exceptions
{
    internal class UtilFilterException : Exception
    {
        public UtilFilterException(string? message = default, Exception? innerException = default) : base(message, innerException) { }
    }

    internal class UtilFilterCriteriaException : ArgumentException
    {
        public object? Criteria { get; set; }

        public UtilFilterCriteriaException(string? message = default, object? criteria = default, string? paramName = default,
            Exception? innerException = default) : base(message, paramName, innerException)
        {
            Criteria = criteria;
        }
    }

    internal class UtilSortException : Exception
    {
        public UtilSortException(string? message = default, Exception? innerException = default) : base(message, innerException) { }
    }
}
