namespace StarRailAPI.Common
{
    public class Result
    {
        private Result(bool isSuccess, Error error, string message)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error");
            }

            IsSuccess = isSuccess;
            Error = error;
            Message = message;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; set; }
        public Error Error { get; }

        public static Result Success(string message)
        {
            return new Result(true, Error.None, message);
        }
        public static Result Failure(Error error)
        {
            return new Result(false, error, string.Empty);
        }
    }
}