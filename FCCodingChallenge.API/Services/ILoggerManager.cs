using System;

namespace FCCodingChallenge.API.Services
{
    public interface ILoggerManager 
    {
        void Information(string message);
        void Debug(string message);
        void Warning(string message);
        void Error(object message, Exception exception);
        void Error(string message, Exception exception);
    }
}
