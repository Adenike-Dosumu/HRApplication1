namespace HRApplication1.Auth
{
    public class Result
    {
        public string? Status { get; set; }
        public string Message { get; set; }
        public object entity { get; set; }

        public bool Succeeded { get; set; }
        public string[] Messages { get; set; }
        public Result()
        {

        }
        internal Result(bool succeeded, IEnumerable<string> messages)
        {
            Succeeded = succeeded;
            Messages = messages.ToArray();
        }
        internal Result(bool succeeded, object entity, IEnumerable<string> messages)
        {
            Succeeded = succeeded;
            this.entity = entity;
            Messages = messages.ToArray();
        }

        internal Result(bool succeeded, IEnumerable<string> messages, object entity)
        {
            Succeeded = succeeded;
            this.entity = entity;
            Messages = messages.ToArray();
        }

        internal Result(bool succeeded, string message, object result = null)
        {
            Succeeded = succeeded;
            Message = message;
        }

        internal Result(bool succeeded, object result)
        {
            Succeeded = succeeded;
            entity = result;
        }

        public static Result Failure(object message)
        {
            throw new NotImplementedException();
        }

        public static Result Success()
        {
            return new Result(true, "Operation Successful");
        }
        public static Result Success(string message)
        {
            return new Result(true, new string[] { message });
        }

        public static Result Success(string message, object entity)
        {
            return new Result(true, message, entity);
        }

        public static Result Success(object entity)
        {
            return new Result(true, entity);
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public static Result Failure(string error)
        {
            return new Result(false, error);
        }
    }
}

