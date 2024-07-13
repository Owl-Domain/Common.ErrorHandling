namespace OwlDomain.Common.Results.Components;

/// <summary>
///   Represents the value component of a result.
/// </summary>
public interface IValueResult
{
   #region Methods
   /// <summary>Checks whether this result is in an okay state.</summary>
   /// <returns><see langword="true"/> if this result is in an okay state, <see langword="false"/> otherwise.</returns>
   bool IsOk();
   #endregion
}
