using System;
using System.Collections.Generic;
using System.Text;

namespace Prism.ThrottleDebounce.SourceGenerators
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThrottleCommandAttribute : Attribute
    {
        public int DelayMs { get; }
        public bool Leading { get; set; } = true;
        public bool Trailing { get; set; } = true;

        public ThrottleCommandAttribute(int delayMs) => DelayMs = delayMs;
    }
}
