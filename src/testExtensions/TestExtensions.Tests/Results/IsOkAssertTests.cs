using OwlDomain.Common.Results;

namespace TestExtensions.Tests.Results;

[TestClass]
public sealed class IsOkAssertTests
{
   #region Fields
   private readonly Mock<IAssert> _assert = new();
   #endregion

   #region IsOk tests
   [TestMethod]
   public void IsOk_WithOkResult_DoesNothing()
   {
      // Arrange
      Result result = new(true);

      // Act
      IAssert assert = ResultAssertExtensions.IsOk(_assert.Object, result);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(assert, _assert.Object);
   }

   [TestMethod]
   public void IsOk_WithErrorResult_CallsFail()
   {
      // Arrange
      Exception error = new();
      Result result = new(error);

      // Act
      IAssert assert = ResultAssertExtensions.IsOk(_assert.Object, result);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(assert, _assert.Object);
   }

   [TestMethod]
   public void IsOk_OutResult_WithOkResult_DoesNothingAndReturnsExpectedValue()
   {
      // Arrange
      const bool expectedValue = true;
      Result result = new(expectedValue);

      // Act
      IAssert assert = ResultAssertExtensions.IsOk(_assert.Object, result, out bool resultValue);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That
         .AreSameInstance(assert, _assert.Object)
         .AreEqual(resultValue, expectedValue);
   }

   [TestMethod]
   public void IsOk_OutResult_WithErrorResult_CallsFail()
   {
      // Arrange
      Exception error = new();
      Result result = new(error);

      // Act
      IAssert assert = ResultAssertExtensions.IsOk(_assert.Object, result, out _);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(assert, _assert.Object);
   }
   #endregion

   #region IsNotOk tests
   [TestMethod]
   public void IsNotOk_WithErrorResult_DoesNothing()
   {
      // Arrange
      Exception error = new();
      Result value = new(error);

      // Act
      IAssert result = ResultAssertExtensions.IsNotOk(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsNotOk_WithOkResult_CallsFail()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert result = ResultAssertExtensions.IsNotOk(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsNotOk_OutResult_WithErrorResult_DoesNothingAndReturnsExpectedError()
   {
      // Arrange
      Exception expectedError = new();
      Result value = new(expectedError);

      // Act
      IAssert result = ResultAssertExtensions.IsNotOk(_assert.Object, value, out Exception resultError);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That
         .AreSameInstance(result, _assert.Object)
         .AreSameInstance(resultError, expectedError);
   }

   [TestMethod]
   public void IsNotOk_OutResult_WithOkResult_CallsFail()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert result = ResultAssertExtensions.IsNotOk(_assert.Object, value, out _);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(result, _assert.Object);
   }
   #endregion
}
