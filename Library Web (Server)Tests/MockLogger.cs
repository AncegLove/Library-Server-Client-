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
        public List<string> LoggedMessages { get; } = new List<string>();
        public IDisposable? BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel level) => true;

        public void Log<TState>(LogLevel level, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formater)
        {
            if (formater != null)
            {
                LoggedMessages.Add(formater(state, exception));
            }
        }
    }
}
