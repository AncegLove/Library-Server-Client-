using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Library_Web__Server_Tests
{
    public class MockLogger<T> : ILogger<T>
    {
        public List<string> LoggedMeassages { get; } = new List<string>();
        public IDisposable? BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel level) => true;

        public void Log<TState>(LoggerLevel level, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formater)
        {
            if (formater != null)
            {
                LoggedMeassages.Add(formater(state, exception));
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}
