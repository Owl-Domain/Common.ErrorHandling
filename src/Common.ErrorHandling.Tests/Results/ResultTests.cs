using OwlDomain.Common.Results;

namespace OwlDomain.Common.ErrorHandling.Tests.Results;

[TestClass]
public sealed class ResultTests : ResultTestsBase<Result, bool, Exception>
{
   #region Unwrap tests
   [TestMethod]
   public void Unwrap_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedValue = true;
      Result sut = new(expectedValue);

      // Act
      bool result = sut.Unwrap();

      // Assert
      Assert.That.AreEqual(result, expectedValue);
   }

   [TestMethod]
   public void Unwrap_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result sut = new(expectedError);

      // Act
      void Act() => sut.Unwrap();

      // Assert
      Assert.That
         .ThrowsExactException(Act, out TestException resultError)
         .AreSameInstance(resultError, expectedError);
   }
   #endregion

   #region Helpers
   protected override bool CreateOkayValue() => true;
   protected override Exception CreateErrorValue() => new();
   protected override Result CreateOkayResult(bool value) => new(value);
   protected override Result CreateErrorResult(Exception error) => new(error);
   #endregion
}
