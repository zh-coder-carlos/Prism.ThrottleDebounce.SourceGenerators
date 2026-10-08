using System;
using System.Collections.Generic;
using System.Text;

namespace Prism.ThrottleDebounce.SourceGenerators
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DebounceCommandAttribute : Attribute
    {
        public int DelayMs { get; }
        public bool Leading { get; set; } = false;
        public bool Trailing { get; set; } = true;

        public DebounceCommandAttribute(int delayMs) => DelayMs = delayMs;
    }
}
