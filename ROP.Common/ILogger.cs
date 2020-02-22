namespace ROP.Common
{
    public interface ILogger : IAppService
    {
        void Debug(string text);
    }
}