using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGenerators.Toolkit.Prism
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ThrottleCommandAttribute : Attribute
    {
        public string? CanExecute { get; set; }

        public int DelayMs { get; }
        public bool Leading { get; set; } = true;
        public bool Trailing { get; set; } = true;

        public ThrottleCommandAttribute(int delayMs) => DelayMs = delayMs;

    }
}
