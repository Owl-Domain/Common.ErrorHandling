using OwlDomain.Common.Results;

namespace OwlDomain.Common.ErrorHandling.Tests.Results;

public abstract class ResultTestsBase<TResult, TValue, TError>
   where TResult : IResult<TValue, TError>
   where TError : notnull
{
   #region IsOk tests
   [TestMethod]
   public void IsOk_WithValue_ReturnsTrue()
   {
      // Arrange
      TValue value = CreateOkayValue();
      TResult sut = CreateOkayResult(value);

      // Act
      bool result = sut.IsOk();

      // Assert
      Assert.That.IsTrue(result);
   }

   [TestMethod]
   public void IsOk_WithError_ReturnsFalse()
   {
      // Arrange
      TError error = CreateErrorValue();
      TResult sut = CreateErrorResult(error);

      // Act
      bool result = sut.IsOk();

      // Assert
      Assert.That.IsFalse(result);
   }

   [TestMethod]
   public void IsOk_WithValue_ReturnsTrueAndValue()
   {
      // Arrange
      TValue expectedValue = CreateOkayValue();
      TResult sut = CreateOkayResult(expectedValue);

      // Act
      bool result = sut.IsOk(out TValue? resultValue);

      // Assert
      Assert.That
         .IsTrue(result)
         .AreEqual(resultValue, expectedValue);
   }

   [TestMethod]
   public void IsOk_WithError_ReturnsFalseAndDefaultValue()
   {
      // Arrange
      TValue? expectedValue = default;

      TError error = CreateErrorValue();
      TResult sut = CreateErrorResult(error);

      // Act
      bool result = sut.IsOk(out TValue? resultValue);

      // Assert
      Assert.That
         .IsFalse(result)
         .AreEqual(resultValue, expectedValue);
   }

   [TestMethod]
   public void IsOk_WithValue_ReturnsTrueAndValueAndNullError()
   {
      // Arrange
      TValue expectedValue = CreateOkayValue();
      TResult sut = CreateOkayResult(expectedValue);

      // Act
      bool result = sut.IsOk(out TValue? resultValue, out TError? error);

      // Assert
      Assert.That
         .IsTrue(result)
         .AreEqual(resultValue, expectedValue)
         .IsNull(error);
   }

   [TestMethod]
   public void IsOk_WithError_ReturnsFalseAndDefaultValueAndError()
   {
      // Arrange
      TValue? expectedValue = default;

      TError expectedError = CreateErrorValue();
      TResult sut = CreateErrorResult(expectedError);

      // Act
      bool result = sut.IsOk(out TValue? resultValue, out TError? resultError);

      // Assert
      Assert.That
         .IsFalse(result)
         .AreEqual(resultValue, expectedValue)
         .AreEqual(resultError, expectedError);
   }
   #endregion

   #region IsError tests
   [TestMethod]
   public void IsError_WithError_ReturnsTrue()
   {
      // Arrange
      TError error = CreateErrorValue();
      TResult sut = CreateErrorResult(error);

      // Act
      bool result = sut.IsError();

      // Assert
      Assert.That.IsTrue(result);
   }

   [TestMethod]
   public void IsError_WithValue_ReturnsFalse()
   {
      // Arrange
      TValue value = CreateOkayValue();
      TResult sut = CreateOkayResult(value);

      // Act
      bool result = sut.IsError();

      // Assert
      Assert.That.IsFalse(result);
   }

   [TestMethod]
   public void IsError_WithError_ReturnsTrueAndError()
   {
      // Arrange
      TError expectedError = CreateErrorValue();
      TResult sut = CreateErrorResult(expectedError);

      // Act
      bool result = sut.IsError(out TError? resultError);

      // Assert
      Assert.That
         .IsTrue(result)
         .AreEqual(resultError, expectedError);
   }

   [TestMethod]
   public void IsError_WithValue_ReturnsFalseAndNullError()
   {
      // Arrange
      TValue value = CreateOkayValue();
      TResult sut = CreateOkayResult(value);

      // Act
      bool result = sut.IsError(out TError? resultError);

      // Assert
      Assert.That
         .IsFalse(result)
         .IsNull(resultError);
   }

   [TestMethod]
   public void IsError_WithError_ReturnsTrueAndErrorAndDefaultValue()
   {
      // Arrange
      TValue? expectedValue = default;
      TError expectedError = CreateErrorValue();

      TResult sut = CreateErrorResult(expectedError);

      // Act
      bool result = sut.IsError(out TError? resultError, out TValue? resultValue);

      // Assert
      Assert.That
         .IsTrue(result)
         .AreEqual(resultError, expectedError)
         .AreEqual(resultValue, expectedValue);
   }

   [TestMethod]
   public void IsError_WithValue_ReturnsFalseAndNullErrorAndValue()
   {
      // Arrange
      TValue expectedValue = CreateOkayValue();
      TResult sut = CreateOkayResult(expectedValue);

      // Act
      bool result = sut.IsError(out TError? resultError, out TValue? resultValue);

      // Assert
      Assert.That
         .IsFalse(result)
         .IsNull(resultError)
         .AreEqual(resultValue, expectedValue);
   }
   #endregion

   #region Helpers
   protected abstract TValue CreateOkayValue();
   protected abstract TError CreateErrorValue();
   protected abstract TResult CreateOkayResult(TValue value);
   protected abstract TResult CreateErrorResult(TError error);
   #endregion
}
