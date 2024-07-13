namespace OwlDomain.Common.Results;

/// <summary>
///   Represents a result value.
/// </summary>
public interface IResult
{
   #region Methods
   /// <summary>Checks whether this result is in an okay state.</summary>
   /// <returns><see langword="true"/> if this result is in an okay state, <see langword="false"/> otherwise.</returns>
   bool IsOk();

   /// <summary>Checks whether this result is in an errored state.</summary>
   /// <returns><see langword="true"/> if this result is in an errored state, <see langword="false"/> otherwise.</returns>
   bool IsError();
   #endregion
}