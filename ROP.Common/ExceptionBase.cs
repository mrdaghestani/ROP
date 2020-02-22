namespace ROP.Common
{
    public abstract class ExceptionBase : System.Exception
    {
        public ExceptionBase(int code, int httpStatusCode, string defaultMessage = null, object[] messageParams = null, System.Exception innerException = null)
        : base(defaultMessage, innerException)
        {
            Code = code;
            HttpStatusCode = httpStatusCode;
            MessageParams = messageParams;
        }
        public int HttpStatusCode { get; private set; }
        public int Code { get; private set; }
        public object[] MessageParams { get; private set; }

        public override string ToString()
        {
            return string.Format($"({Code}:{HttpStatusCode}) -> {Message}", MessageParams ?? new[] { "" });
        }
    }
}