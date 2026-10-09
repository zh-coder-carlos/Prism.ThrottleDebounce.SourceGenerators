using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGenerators.Toolkit.Prism
{
    public sealed class CommandGenerationInfo
    {
        public string NamespaceName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string MethodName { get; set; } = string.Empty;
        public string DelayMs { get; set; } = "300";
        public bool IsDebounce { get; set; }
        public string Leading { get; set; } = "false";
        public string Trailing { get; set; } = "true";
        public string CanExecute { get; set; } = "null";
        public string GenericArgs { get; set; } = string.Empty;
        public string ParamList { get; set; } = string.Empty;
        public string DelegateParamList { get; set; } = string.Empty;
    }

}
