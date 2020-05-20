using System;

namespace ObjectiveManagement.Domain.Contracts.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
            
        }
    }
}