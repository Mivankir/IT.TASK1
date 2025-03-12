using System;

namespace AvaloniaLogManagerApp
{
    public enum LogType
    {
        Error,
        Warning,
        Info
    }

    public struct LogMessage
    {
        public LogType Type { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"[{Type}] {DateTime}: {Text}";
        }
    }
}