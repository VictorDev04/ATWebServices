namespace ATCUDVALIDATOR.API.Helpers
{
    public class ExecutionResult<T> : ExecutionResult
    {
        public ExecutionResult()
        {
        }

        public ExecutionResult(string key, string message, bool success, T result) : base(key, message, success)
        {
            this.Result = result;
        }

        public T Result { get; set; }
    }

    public class ExecutionResult
    {
        public ExecutionResult()
        {
        }

        public ExecutionResult(string key, string message, bool success)
        {
            this.Key = key;
            this.Message = message;
            this.Success = success;
        }

        public string Message { get; set; }

        public string Key { get; set; }

        public bool Success { get; set; }


        public ExecutionResult SuccessResult()
        {
            return new ExecutionResult("", "", true);
        }
    }
}
