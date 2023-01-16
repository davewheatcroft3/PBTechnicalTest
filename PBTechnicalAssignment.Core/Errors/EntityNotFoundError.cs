using FluentResults;

namespace PBTechnicalAssignment.Core.Errors
{
    public class EntityNotFoundError : IError
    {
        public List<IError> Reasons => new List<IError>();

        public string Message => "Entity not found";

        public Dictionary<string, object> Metadata => new Dictionary<string, object>();
    }
}
