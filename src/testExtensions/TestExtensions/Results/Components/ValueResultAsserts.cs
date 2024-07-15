namespace OwlDomain.Common.Results.Components;

public static partial class ResultComponentAssertExtensions
{
   #region Constants
   private const string IsOkFormat = "{0} was expect to be in an okay state, but it wasn't.\nLine: {1}";
   private const string IsNotOkFormat = "{0} was in an okay state, when it wasn't expected to be.\nLine: {1}";
   private const string IsNotOkWithValueFormat = "{0} was in an okay state, when it wasn't expected to be.\nResult value: {1}\nLine: {2}";
   #endregion

   #region IsOk methods
   /// <summary>Asserts that the given <paramref name="result"/> is in an okay state.</summary>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsOk(
      this IAssert assert,
      IValueResult result,
      [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
      [CallerLineNumber] int line = 0)
   {
      if (result.IsOk() is false)
         assert.Fail(IsOkFormat, resultArgument, line);

      return assert;
   }

   /// <summary>Asserts that the given <paramref name="result"/> is in an okay state.</summary>
   /// <typeparam name="TValue">The type of the <paramref name="value"/> stored in the <paramref name="result"/>.</typeparam>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="value">The value that was stored in the <paramref name="result"/>.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsOk<TValue>(
      this IAssert assert,
      IValueResult<TValue> result,
      out TValue value,
      [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
      [CallerLineNumber] int line = 0)
   {
      if (result.IsOk(out TValue? val) is false)
         assert.Fail(IsOkFormat, resultArgument, line);

      value = val;
      return assert;
   }
   #endregion

   #region IsNotOk methods
   /// <summary>Asserts that the given <paramref name="result"/> is not in an okay state.</summary>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   public static IAssert IsNotOk(
      this IAssert assert,
      IValueResult result,
      [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
      [CallerLineNumber] int line = 0)
   {
      if (result.IsOk())
         assert.Fail(IsNotOkFormat, resultArgument, line);

      return assert;
   }

   /// <summary>Asserts that the given <paramref name="result"/> is not in an okay state.</summary>
   /// <typeparam name="TValue">The type of the value stored in the <paramref name="result"/>.</typeparam>
   /// <param name="assert">The assertion instance.</param>
   /// <param name="result">The result to check.</param>
   /// <param name="resultArgument">The argument expression that was passed in as the <paramref name="result"/>.</param>
   /// <param name="line">The line in the source file where this assertion was made.</param>
   /// <returns>The <see cref="IAssert"/> instance this extension method was called on to allow for chaining assertions.</returns>
   /// <remarks>
   ///   This overload is preferred over the non-generic one as it can provide a more descriptive 
   ///   failure message by providing the value stored in the <paramref name="result"/>.
   /// </remarks>
   public static IAssert IsNotOk<TValue>(
      this IAssert assert,
      IValueResult<TValue> result,
      [CallerArgumentExpression(nameof(result))] string resultArgument = "<result>",
      [CallerLineNumber] int line = 0)
   {
      if (result.IsOk(out TValue? value))
         assert.Fail(IsNotOkWithValueFormat, resultArgument, value, line);

      return assert;
   }
   #endregion
}
