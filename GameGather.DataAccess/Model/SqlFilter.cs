using System;
using System.Collections.Generic;

namespace GameGather.DataAccess.Model
{
    internal class SqlFilter
    {
        private List<string> Conditions { get; set; } = new List<string>();
        private Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public void AddCondition(string text, string? parameterName = null, object? parameterValue = null) 
        {
            Conditions.Add(text);
            if (parameterName != null && parameterValue != null)
                Parameters[parameterName] = parameterValue;
        }

        public string GetWhereText() 
        {
            if (Conditions.Count == 0)
                return "";
            return $" Where {string.Join(" and ", Conditions)} ";
        }

        public Dictionary<string, object> GetParameters() => Parameters;
    }
}
