namespace OwlDomain.Common.Results.Components;

public static partial class ResultComponentAssertExtensions
{
   #region Constants
   private const string IsErrorFormat = "{0} was expected to be in an errored state, but it wasn't.\nLine {1}";
   private const string IsNotErrorFormat = "{0} was in an errored state, when it wasn't expected to be.\nLine: {1}";
   private const string IsNotErrorWithErrorFormat = "{0} was in an errored state, when it wasn't expected to be.\nResult error: {1}\nLine: {2}";
   #endregion

   #region IsError methods
   /// <summary>Asserts that the given <paramref name="result"/> is in an errored state.</summary>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsError(
      this IAssert assert,
      IErrorResult result,
      [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
      [CallerLineNumber] int line = 0)
   {
      if (result.IsError() is false)
         assert.Fail(IsErrorFormat, resultArgument, line);

      return assert;
   }

   /// <summary>Asserts that the given <paramref name="result"/> is in an errored state.</summary>
   /// <typeparam name="TError">The type of the <paramref name="error"/> stored in the <paramref name="result"/>.</typeparam>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="error">The value that was stored in the <paramref name="result"/>.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsError<TError>(
      this IAssert assert,
      IErrorResult<TError> result,
      out TError error,
      [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
      [CallerLineNumber] int line = 0)
      where TError : notnull
   {
      if (result.IsError(out TError? err) is false)
         assert.Fail(IsErrorFormat, resultArgument, line);

      error = err;
      return assert;
   }
   #endregion

   #region IsNotError methods
   /// <summary>Asserts that the given <paramref name="result"/> is not in an errored state.</summary>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsNotError(
      this IAssert assert,
      IErrorResult result,
      [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
      [CallerLineNumber] int line = 0)
   {
      if (result.IsError())
         assert.Fail(IsNotErrorFormat, resultArgument, line);

      return assert;
   }

   /// <summary>Asserts that the given <paramref name="result"/> is not in an errored state.</summary>
   /// <typeparam name="TError">The type of the error stored in the <paramref name="result"/>.</typeparam>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   /// <remarks>
   ///   This overload is preferred over the non-generic one as it can provide a more descriptive
   ///   failure message by providing the error stored in the <paramref name="result"/>.
   /// </remarks>
   public static IAssert IsNotError<TError>(
      this IAssert assert,
      IErrorResult<TError> result,
      [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
      [CallerLineNumber] int line = 0)
      where TError : notnull
   {
      if (result.IsError(out TError? error))
         assert.Fail(IsNotErrorWithErrorFormat, resultArgument, error, line);

      return assert;
   }
   #endregion
}
