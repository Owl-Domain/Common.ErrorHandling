namespace OwlDomain.Common.Results.Components;

/// <summary>
///   Represents the value component of a result.
/// </summary>
/// <typeparam name="TValue">The type of the possible value, can be nullable.</typeparam>
public interface IValueResult<TValue> : IValueResult
{
   #region Methods
   /// <summary>Checks whether this result is in an okay state.</summary>
   /// <param name="value">The value stored in this result.</param>
   /// <returns><see langword="true"/> if this result is in an okay state, <see langword="false"/> otherwise.</returns>
   bool IsOk([MaybeNullWhen(false)] out TValue value);
   #endregion
}
