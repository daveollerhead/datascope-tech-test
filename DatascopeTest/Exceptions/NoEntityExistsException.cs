using System;

namespace DatascopeTest.Exceptions
{
    public class NoEntityExistsException : ApplicationException
    {
        public NoEntityExistsException(string entityName, object entityId)
            :base($"Could not find {entityName} with id {entityId}")
        {
        }
    }
}