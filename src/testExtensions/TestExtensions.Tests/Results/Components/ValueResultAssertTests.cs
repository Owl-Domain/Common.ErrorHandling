using OwlDomain.Common.Results;
using OwlDomain.Common.Results.Components;

namespace TestExtensions.Tests.Results.Components;

[TestClass]
public sealed class ValueResultAssertTests
{
   #region Fields
   private readonly Mock<IAssert> _assert = new();
   #endregion

   #region IsOk tests
   [TestMethod]
   public void IsOk_WithOkResult_DoesNothing()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsOk(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsOk_WithErrorResult_CallsFail()
   {
      // Arrange
      Exception error = new();
      Result value = new(error);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsOk(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsOk_Generic_WithOkResult_DoesNothingAndReturnsExpectedValue()
   {
      // Arrange
      const bool expectedValue = true;
      Result value = new(expectedValue);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsOk(_assert.Object, value, out bool resultValue);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That
         .AreSameInstance(result, _assert.Object)
         .AreEqual(resultValue, expectedValue);
   }

   [TestMethod]
   public void IsOk_Generic_WithErrorResult_CallsFail()
   {
      // Arrange
      Exception error = new();
      Result value = new(error);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsOk(_assert.Object, value, out _);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(result, _assert.Object);
   }
   #endregion

   #region IsNotOk tests
   [TestMethod]
   public void IsNotOk_WithOkResult_CallsFail()
   {
      // Arrange
      IValueResult value = new Result(true);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsNotOk(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsNotOk_WithErrorResult_DoesNothing()
   {
      // Arrange
      Exception error = new();
      IValueResult value = new Result(error);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsNotOk(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsNotOk_Generic_WithOkResult_CallsFail()
   {
      // Arrange
      Result value = new(true);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsNotOk<bool>(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Once());

      Assert.That.AreSameInstance(result, _assert.Object);
   }

   [TestMethod]
   public void IsNotOk_Generic_WithErrorResult_DoesNothing()
   {
      // Arrange
      Exception error = new();
      Result value = new(error);

      // Act
      IAssert result = ResultComponentAssertExtensions.IsNotOk<bool>(_assert.Object, value);

      // Assert
      _assert.VerifyFailFormatOnly(Times.Never());

      Assert.That.AreSameInstance(result, _assert.Object);
   }
   #endregion
}
