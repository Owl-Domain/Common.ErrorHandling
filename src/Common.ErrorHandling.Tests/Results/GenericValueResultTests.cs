using OwlDomain.Common.Results;

namespace OwlDomain.Common.ErrorHandling.Tests.Results;

[TestClass]
public sealed class GenericValueResultTests : ResultTestsBase<Result<bool>, bool, Exception>
{
   #region Unwrap tests
   [TestMethod]
   public void Unwrap_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedValue = true;
      Result<bool> sut = new(expectedValue);

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
      Result<bool> sut = new(expectedError);

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
   protected override Result<bool> CreateOkayResult(bool value) => new(value);
   protected override Result<bool> CreateErrorResult(Exception error) => new(error);
   #endregion
}
