using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace DDD.Application.Exceptions;

public class ValidateException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidateException() : base("One or more validation failures.")
	{
		Errors = new Dictionary<string, string[]>();
	}

	public ValidateException(IEnumerable<ValidationFailure> failures) : this()
	{
        Errors = failures
			.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
			.ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

	public ValidateException(IEnumerable<IdentityError> errors) : this()
	{
        Errors = errors
             .GroupBy(e => e.Code, e => e.Description)
             .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
	 
}
