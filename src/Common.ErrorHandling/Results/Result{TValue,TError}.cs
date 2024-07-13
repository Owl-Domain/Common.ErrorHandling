namespace OwlDomain.Common.Results;

/// <inheritdoc cref="IResult{TValue, TError}"/>
public readonly struct Result<TValue, TError> : IResult<TValue, TError>
   where TError : notnull
{
   #region Fields
   private readonly TValue? _value;
   private readonly TError? _error;
   #endregion

   #region Constructors
   /// <summary>Creates a result in an okay state, with the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to store in the result.</param>
   public Result(TValue value) => _value = value;

   /// <summary>Creates a result in an errored state, with the given <paramref name="error"/>.</summary>
   /// <param name="error">The error to store in the result.</param>
   public Result(TError error) => _error = error;
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
   public bool IsOk([MaybeNullWhen(false)] out TValue value, [NotNullWhen(false)] out TError? error)
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
   public bool IsError([NotNullWhen(true)] out TError? error)
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
   public bool IsError([NotNullWhen(true)] out TError? error, [MaybeNullWhen(true)] out TValue value)
   {
      if (_error is null)
      {
         value = _value!;
         error = default;

         return false;
      }

      value = default;
      error = _error;

      return true;
   }
   #endregion
}

/// <summary>
///   Contains useful extension methods related to the <see cref="Result"/>,
///   <see cref="Result{TValue}"/> and <see cref="Result{TValue, TError}"/> types.
/// </summary>
public static partial class ResultExtensions
{
   #region Methods
   /// <summary>
   ///   If the result is in an okay state, then it returns the stored value,
   ///   otherwise, it throws the error.
   /// </summary>
   /// <typeparam name="TValue">The type of the possible value, can be nullable.</typeparam>
   /// <typeparam name="TError">The type of the possible error, cannot be nullable and must be derived from <see cref="Exception"/>.</typeparam>
   /// <param name="result">The result to unwrap.</param>
   /// <returns>The value stored in the result.</returns>
   /// <exception cref="Exception">Thrown if the result is in an errored state.</exception>
   [MethodImpl(MethodImplOptions.NoInlining)]
   public static TValue Unwrap<TValue, TError>(this Result<TValue, TError> result)
      where TError : notnull, Exception
   {
      if (result.IsOk(out TValue? value, out TError? error))
         return value;

      throw error;
   }

   /// <summary>
   ///   If the result is in an okay state, then it returns the stored value,
   ///   otherwise, it throws the error.
   /// </summary>
   /// <typeparam name="TValue">The type of the possible value, can be nullable.</typeparam>
   /// <typeparam name="TError">The type of the possible error, cannot be nullable and must be derived from <see cref="Exception"/>.</typeparam>
   /// <param name="value">The result to <see langword="await"/> and then unwrap.</param>
   /// <returns>The value stored in the result.</returns>
   /// <exception cref="Exception">Thrown if the result is in an errored state.</exception>
   public static async ValueTask<TValue> UnwrapAsync<TValue, TError>(this ValueTask<Result<TValue, TError>> value)
      where TError : notnull, Exception
   {
      Result<TValue, TError> result = await value;
      return result.Unwrap();
   }

   /// <summary>
   ///   If the result is in an okay state, then it returns the stored value,
   ///   otherwise, it throws the error.
   /// </summary>
   /// <typeparam name="TValue">The type of the possible value, can be nullable.</typeparam>
   /// <typeparam name="TError">The type of the possible error, cannot be nullable and must be derived from <see cref="Exception"/>.</typeparam>
   /// <param name="value">The result to <see langword="await"/> and then unwrap.</param>
   /// <returns>The value stored in the result.</returns>
   /// <exception cref="Exception">Thrown if the result is in an errored state.</exception>
   public static async ValueTask<TValue> UnwrapAsync<TValue, TError>(this Task<Result<TValue, TError>> value)
      where TError : notnull, Exception
   {
      Result<TValue, TError> result = await value;
      return result.Unwrap();
   }
   #endregion
}