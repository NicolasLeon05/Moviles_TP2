public interface ILoggerService
{
    void Record(string level, string message, string stack = "");
    string GetAll();
    void RequestClear();
}
