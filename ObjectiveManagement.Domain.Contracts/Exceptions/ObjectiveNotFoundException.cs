using System;

namespace ObjectiveManagement.Domain.Contracts.Exceptions
{
    public class ObjectiveNotFoundException : Exception
    {
        public ObjectiveNotFoundException(string message)
        : base(message)
        {
            
        }
    }
}
