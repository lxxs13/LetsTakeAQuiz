namespace GeekQuiz.Helpers
{
    public class ServiceResult<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data) => new ServiceResult<T>
        {
            Status = true,
            Data = data
        };

        public static ServiceResult<T> Fail(string errorMessage) => new ServiceResult<T>
        {
            Status = false,
            Message = errorMessage
        };
    }
}
