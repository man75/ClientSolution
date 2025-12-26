using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSi.APPLICATION.Services
{
    public class Notification
    {
        private readonly Dictionary<string, string[]> _errors = new();
        public Dictionary<string, string[]> Errors => _errors;

        public bool HasErrors => _errors.Any();
        public void AddError(string key, string message)
        {
            if (_errors.TryGetValue(key, out var messages))
            {
                _errors[key] = messages.Concat(new[] { message }).ToArray();
            }
            else
                _errors[key] = new[] { message };
            }
        
    }
}