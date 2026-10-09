using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGenerators.Toolkit.Prism
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class DebounceCommandAttribute : Attribute
    {
        public int DelayMs { get; }
        public bool Leading { get; set; } = false;
        public bool Trailing { get; set; } = true;

        public DebounceCommandAttribute(int delayMs) => DelayMs = delayMs;
    }
}
