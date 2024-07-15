using OwlDomain.Common.Results;

namespace TestExtensions.Tests.Results;

[TestClass]
public sealed class IsErrorAssertTests
{
   #region Fields
   private readonly Mock<IAssert> _assert = new();
   #endregion

   #region IsError tests
   [TestMethod]
   public void IsError_WithErrorResult_DoesNothing()
   {
      // Arrange
      Exception error = new();
      Result value = new(error);

      // Act
      IAssert assert = ResultAssertExtensions.IsError(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(assert, _assert.Object);
   }

   [TestMethod]
   public void IsError_WithOkResult_CallsFail()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert assert = ResultAssertExtensions.IsError(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(assert, _assert.Object);
   }

   [TestMethod]
   public void IsError_OutResult_WithErrorResult_DoesNothingAndReturnsExpectedError()
   {
      // Arrange
      Exception expectedError = new();
      Result value = new(expectedError);

      // Act
      IAssert assert = ResultAssertExtensions.IsError(_assert.Object, value, out Exception resultError);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That
         .AreSameInstance(assert, _assert.Object)
         .AreSameInstance(resultError, expectedError);
   }

   [TestMethod]
   public void IsError_OutResult_WithOkResult_CallsFail()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert assert = ResultAssertExtensions.IsError(_assert.Object, value, out _);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(assert, _assert.Object);
   }
   #endregion

   #region IsNotError tests
   [TestMethod]
   public void IsNotError_WithOkResult_DoesNothing()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert assert = ResultAssertExtensions.IsNotError(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(assert, _assert.Object);
   }

   [TestMethod]
   public void IsNotError_WithErrorResult_CallsFail()
   {
      // Arrange
      Exception error = new();
      Result value = new(error);

      // Act
      IAssert assert = ResultAssertExtensions.IsNotError(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(assert, _assert.Object);
   }

   [TestMethod]
   public void IsNotError_OutResult_WithOkResult_DoesNothingAndReturnsExpectedValue()
   {
      // Arrange
      const bool expectedValue = true;
      Result value = new(expectedValue);

      // Act
      IAssert assert = ResultAssertExtensions.IsNotError(_assert.Object, value, out bool resultValue);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That
         .AreSameInstance(assert, _assert.Object)
         .AreEqual(resultValue, expectedValue);
   }

   [TestMethod]
   public void IsNotError_OutResult_WithErrorResult_CallsFail()
   {
      // Arrange
      Exception error = new();
      Result value = new(error);

      // Act
      IAssert assert = ResultAssertExtensions.IsNotError(_assert.Object, value, out _);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(assert, _assert.Object);
   }
   #endregion
}
