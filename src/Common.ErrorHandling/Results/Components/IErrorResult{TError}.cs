namespace OwlDomain.Common.Results.Components;

/// <summary>
///   Represents the error component of a result.
/// </summary>
/// <typeparam name="TError">The type of the possible error, cannot be nullable.</typeparam>
public interface IErrorResult<TError> : IErrorResult
   where TError : notnull
{
   #region Methods
   /// <summary>Checks whether this result is in an errored state.</summary>
   /// <param name="error">The error stored in this result.</param>
   /// <returns><see langword="true"/> if this result is in an errored state, <see langword="false"/> otherwise.</returns>
   bool IsError([NotNullWhen(true)] out TError? error);
   #endregion
}
