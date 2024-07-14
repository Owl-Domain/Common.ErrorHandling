using OwlDomain.Common.Results;

namespace OwlDomain.Common.ErrorHandling.Tests.Results;

[TestClass]
public sealed class GenericValueAndErrorResultTests : ResultTestsBase<Result<bool, Exception>, bool, Exception>
{
   #region Helpers
   protected override bool CreateOkayValue() => true;
   protected override Exception CreateErrorValue() => new();
   protected override Result<bool, Exception> CreateOkayResult(bool value) => new(value);
   protected override Result<bool, Exception> CreateErrorResult(Exception error) => new(error);
   #endregion
}
