namespace OwlDomain.Common.Results;

public static partial class ResultAssertExtensions
{
   #region Constants
   private const string IsErrorFormat = "{0} was expect to be in an errored state, but it wasn't.\nResult value: {1}\nLine: {2}";
   private const string IsNotErrorFormat = "{0} was in an errored state, when it wasn't expected to be.\nResult error: {1}\nLine: {2}";
   #endregion

   #region IsError methods
   /// <summary>Asserts that the given <paramref name="result"/> is in an errored state.</summary>
   /// <typeparam name="TValue">The type of the value stored in the <paramref name="result"/>.</typeparam>
   /// <typeparam name="TError">The type of the error stored in the <paramref name="result"/>.</typeparam>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsError<TValue, TError>(
     this IAssert assert,
     IResult<TValue, TError> result,
     [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
     [CallerLineNumber] int line = 0)
      where TError : notnull
   {
      if (result.IsError(out _, out TValue? value) is false)
         assert.Fail(IsErrorFormat, resultArgument, value, line);

      return assert;
   }

   /// <summary>Asserts that the given <paramref name="result"/> is in an errored state.</summary>
   /// <typeparam name="TValue">The type of the value stored in the <paramref name="result"/>.</typeparam>
   /// <typeparam name="TError">The type of the <paramref name="error"/> stored in the <paramref name="result"/>.</typeparam>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="error">The error stored in the <paramref name="result"/>.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsError<TValue, TError>(
     this IAssert assert,
     IResult<TValue, TError> result,
     out TError error,
     [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
     [CallerLineNumber] int line = 0)
      where TError : notnull
   {
      if (result.IsError(out TError? err, out TValue? value) is false)
         assert.Fail(IsErrorFormat, resultArgument, value, line);

      error = err;
      return assert;
   }
   #endregion

   #region IsNotError methods
   /// <summary>Asserts that the given <paramref name="result"/> is not in an errored state.</summary>
   /// <typeparam name="TValue">The type of the value stored in the <paramref name="result"/>.</typeparam>
   /// <typeparam name="TError">The type of the error stored in the <paramref name="result"/>.</typeparam>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsNotError<TValue, TError>(
     this IAssert assert,
     IResult<TValue, TError> result,
     [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
     [CallerLineNumber] int line = 0)
      where TError : notnull
   {
      if (result.IsError(out TError? error, out _))
         assert.Fail(IsNotErrorFormat, resultArgument, error, line);

      return assert;
   }

   /// <summary>Asserts that the given <paramref name="result"/> is not in an errored state.</summary>
   /// <typeparam name="TValue">The type of the <paramref name="value"/> stored in the <paramref name="result"/>.</typeparam>
   /// <typeparam name="TError">The type of the error stored in the <paramref name="result"/>.</typeparam>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="value">The value stored in the <paramref name="result"/>.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsNotError<TValue, TError>(
     this IAssert assert,
     IResult<TValue, TError> result,
     out TValue value,
     [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
     [CallerLineNumber] int line = 0)
      where TError : notnull
   {
      if (result.IsError(out TError? error, out TValue? val))
         assert.Fail(IsNotErrorFormat, resultArgument, error, line);

      value = val;
      return assert;
   }
   #endregion
}
