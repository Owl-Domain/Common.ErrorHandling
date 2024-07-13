namespace OwlDomain.Common.Results;

/// <summary>
///   Represents a result value, with a possible value of the type <typeparamref name="TValue"/>, 
///   and a possible <see cref="Exception"/> error value.
/// </summary>
/// <typeparam name="TValue">The type of the possible value, can be nullable.</typeparam>
public readonly struct Result<TValue> : IResult<TValue, Exception>
{
   #region Fields
   private readonly TValue? _value;
   private readonly Exception? _error;
   #endregion

   #region Constructors
   /// <summary>Creates a result in an okay state, with the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to store in the result.</param>
   public Result(TValue value) => _value = value;

   /// <summary>Creates a result in an errored state, with the given <paramref name="error"/>.</summary>
   /// <param name="error">The error to store in the result.</param>
   public Result(Exception error) => _error = error;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public bool IsOk() => _error is null;

   /// <inheritdoc/>
   public bool IsOk([MaybeNullWhen(false)] out TValue value)
   {
      if (_error is null)
      {
         value = _value!;
         return true;
      }

      value = default;
      return false;
   }

   /// <inheritdoc/>
   public bool IsOk([MaybeNullWhen(false)] out TValue value, [NotNullWhen(false)] out Exception? error)
   {
      if (_error is null)
      {
         value = _value!;
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
   public bool IsError([NotNullWhen(true)] out Exception? error, [MaybeNullWhen(true)] out TValue value)
   {
      if (_error is null)
      {
         value = _value!;
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
   public TValue Unwrap()
   {
      if (_error is null)
         return _value!;

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
   /// <typeparam name="TValue">The type of the possible value, can be nullable.</typeparam>
   /// <param name="value">The result to <see langword="await"/> and then unwrap.</param>
   /// <returns>The value stored in the result.</returns>
   /// <exception cref="Exception">Thrown if the result is in an errored state.</exception>
   public static async ValueTask<TValue> UnwrapAsync<TValue>(this ValueTask<Result<TValue>> value)
   {
      Result<TValue> result = await value;
      return result.Unwrap();
   }

   /// <summary>
   ///   If the result is in an okay state, then it returns the stored value,
   ///   otherwise, it throws the error.
   /// </summary>
   /// <typeparam name="TValue">The type of the possible value, can be nullable.</typeparam>
   /// <param name="value">The result to <see langword="await"/> and then unwrap.</param>
   /// <returns>The value stored in the result.</returns>
   /// <exception cref="Exception">Thrown if the result is in an errored state.</exception>
   public static async ValueTask<TValue> UnwrapAsync<TValue>(this Task<Result<TValue>> value)
   {
      Result<TValue> result = await value;
      return result.Unwrap();
   }
   #endregion
}