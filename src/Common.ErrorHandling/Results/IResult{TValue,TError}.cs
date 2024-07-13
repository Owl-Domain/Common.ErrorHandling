using OwlDomain.Common.Results.Components;

namespace OwlDomain.Common.Results;

/// <summary>
///   Represents a result value, with a possible value of the type <typeparamref name="TValue"/>, 
///   and a possible error of the type <typeparamref name="TError"/>.
/// </summary>
/// <typeparam name="TValue">The type of the possible value, can be nullable.</typeparam>
/// <typeparam name="TError">The type of the possible error, cannot be nullable.</typeparam>
public interface IResult<TValue, TError> : IResult, IValueResult<TValue>, IErrorResult<TError>
   where TError : notnull
{
   #region Methods
   /// <summary>Checks whether this result is in an okay state.</summary>
   /// <param name="value">The value stored in this result.</param>
   /// <param name="error">The error stored in this result.</param>
   /// <returns><see langword="true"/> if this result is in an okay state, <see langword="false"/> otherwise.</returns>
   bool IsOk([MaybeNullWhen(false)] out TValue value, [NotNullWhen(false)] out TError? error);

   /// <summary>Checks whether this result is in an errored state.</summary>
   /// <param name="error">The error stored in this result.</param>
   /// <param name="value">The value stored in this result.</param>
   /// <returns><see langword="true"/> if this result is in an errored state, <see langword="false"/> otherwise.</returns>
   bool IsError([NotNullWhen(true)] out TError? error, [MaybeNullWhen(true)] out TValue value);
   #endregion
}

/// <summary>
///   Contains useful extension methods related to the <see cref="IResult"/>
///   and <see cref="IResult{TValue, TError}"/> interfaces.
/// </summary>
public static partial class IResultExtensions
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
   public static TValue Unwrap<TValue, TError>(this IResult<TValue, TError> result)
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
   public static async ValueTask<TValue> UnwrapAsync<TValue, TError>(this ValueTask<IResult<TValue, TError>> value)
      where TError : notnull, Exception
   {
      IResult<TValue, TError> result = await value;
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
   public static async ValueTask<TValue> UnwrapAsync<TValue, TError>(this Task<IResult<TValue, TError>> value)
      where TError : notnull, Exception
   {
      IResult<TValue, TError> result = await value;
      return result.Unwrap();
   }
   #endregion
}