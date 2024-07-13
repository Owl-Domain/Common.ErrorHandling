namespace OwlDomain.Common.Results;

/// <summary>
///   Represents a result value, with a possible <see cref="bool"/> 
///   value, and a possible <see cref="Exception"/> error value.
/// </summary>
public readonly struct Result : IResult<bool, Exception>
{
   #region Fields
   private readonly bool _value;
   private readonly Exception? _error;
   #endregion

   #region Constructors
   /// <summary>Creates a result in an okay state, with the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to store in the result.</param>
   public Result(bool value) => _value = value;

   /// <summary>Creates a result in an errored state, with the given <paramref name="error"/>.</summary>
   /// <param name="error">The error to store in the result.</param>
   public Result(Exception error) => _error = error;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public bool IsOk() => _error is null;

   /// <inheritdoc/>
   public bool IsOk(out bool value)
   {
      if (_error is null)
      {
         value = _value;
         return true;
      }

      value = default;
      return false;
   }

   /// <inheritdoc/>
   public bool IsOk(out bool value, [NotNullWhen(false)] out Exception? error)
   {
      if (_error is null)
      {
         value = _value;
         error = default;

         return true;
      }

      value = default;
      error = _error;

      return false;
   }

   /// <inheritdoc/>
   public bool IsError() => _error is not null;

   /// <inheritdoc/>
   public bool IsError([NotNullWhen(true)] out Exception? error)
   {
      if (_error is null)
      {
         error = default;
         return false;
      }

      error = _error;
      return true;
   }

   /// <inheritdoc/>
   public bool IsError([NotNullWhen(true)] out Exception? error, out bool value)
   {
      if (_error is null)
      {
         value = _value;
         error = default;

         return false;
      }

      value = default;
      error = _error!;

      return true;
   }

   /// <summary>
   ///   If the result is in an okay state, then it returns the stored value,
   ///   otherwise, it throws the error.
   /// </summary>
   /// <returns>The value stored in the result.</returns>
   /// <exception cref="Exception">Thrown if the result is in an errored state.</exception>
   [MethodImpl(MethodImplOptions.NoInlining)]
   public bool Unwrap()
   {
      if (_error is null)
         return _value;

      throw _error;
   }
   #endregion
}

public static partial class ResultExtensions
{
   #region Methods
   /// <summary>
   ///   If the result is in an okay state, then it returns the stored value,
   ///   otherwise, it throws the error.
   /// </summary>
   /// <param name="value">The result to <see langword="await"/> and then unwrap.</param>
   /// <returns>The value stored in the result.</returns>
   /// <exception cref="Exception">Thrown if the result is in an errored state.</exception>
   public static async ValueTask<bool> UnwrapAsync(this ValueTask<Result> value)
   {
      Result result = await value;
      return result.Unwrap();
   }

   /// <summary>
   ///   If the result is in an okay state, then it returns the stored value,
   ///   otherwise, it throws the error.
   /// </summary>
   /// <param name="value">The result to <see langword="await"/> and then unwrap.</param>
   /// <returns>The value stored in the result.</returns>
   /// <exception cref="Exception">Thrown if the result is in an errored state.</exception>
   public static async ValueTask<bool> UnwrapAsync(this Task<Result> value)
   {
      Result result = await value;
      return result.Unwrap();
   }
   #endregion
}
