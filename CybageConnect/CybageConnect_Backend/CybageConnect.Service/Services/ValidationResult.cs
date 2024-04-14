using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.Services
{
    public class ValidationResult
    {
        private List<string> errors;

        public bool IsValid => errors.Count == 0;

        public IReadOnlyList<string> Errors => errors;

        public ValidationResult()
        {
            errors = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            errors.Add(errorMessage);
        }
    }
}
