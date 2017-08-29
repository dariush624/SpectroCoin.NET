using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.Exceptions
{
    public class ValidationException : Exception
    {
        private List<ValidationError> _validationErrors;
        public List<ValidationError> ValidationErrors { get => _validationErrors; }

        public ValidationException(string message, List<ValidationError> errors) : base(message)
        {
            _validationErrors = errors;
        }

        public ValidationException(string message) : this(message, null) { }

        public ValidationException(List<ValidationError> errors) : this(null, errors) { }
    }
}
