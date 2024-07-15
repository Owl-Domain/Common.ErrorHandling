using OwlDomain.Common.Results;
using OwlDomain.Common.Results.Components;

namespace TestExtensions.Tests.Results.Components;

[TestClass]
public sealed class ErrorResultAssertTests
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
      IAssert result = ResultComponentAssertExtensions.IsError(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsError_WithOkResult_CallsFail()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsError(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsError_Generic_WithErrorResult_DoesNothingAndReturnsExpectedError()
   {
      // Arrange
      Exception expectedError = new();
      Result value = new(expectedError);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsError(_assert.Object, value, out Exception resultError);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That
         .AreSameInstance(result, _assert.Object)
         .AreSameInstance(resultError, expectedError);
   }

   [TestMethod]
   public void IsError_Generic_WithOkResult_CallsFail()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsError(_assert.Object, value, out _);

      // Assert
      Assert.That.AreSameInstance(result, _assert.Object);
   }
   #endregion

   #region IsNotError tests
   [TestMethod]
   public void IsNotError_WithErrorResult_CallsFail()
   {
      // Arrange
      Exception error = new();
      IErrorResult value = new Result(error);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsNotError(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsNotError_WithOkResult_DoesNothing()
   {
      // Arrange
      IErrorResult value = new Result(true);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsNotError(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsNotError_Generic_WithErrorResult_CallsFail()
   {
      // Arrange
      Exception error = new();
      Result value = new(error);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsNotError<Exception>(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsNotError_Generic_WithOkResult_DoesNothing()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsNotError<Exception>(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(result, _assert.Object);
   }
   #endregion
}
