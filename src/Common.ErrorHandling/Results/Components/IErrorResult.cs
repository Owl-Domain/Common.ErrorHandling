namespace OwlDomain.Common.Results.Components;

/// <summary>
///   Represents the error component of a result.
/// </summary>
public interface IErrorResult
{
   #region Methods
   /// <summary>Checks whether this result is in an errored state.</summary>
   /// <returns><see langword="true"/> if this result is in an errored state, <see langword="false"/> otherwise.</returns>
   bool IsError();
   #endregion
}
