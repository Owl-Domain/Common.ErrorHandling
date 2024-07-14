using System.Threading.Tasks;
using OwlDomain.Common.Results;

namespace OwlDomain.Common.ErrorHandling.Tests.Results;

[TestClass]
public sealed class ResultExtensionsTests
{
   #region Unwrap tests
   [TestMethod]
   public void Unwrap_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result<bool, Exception> value = new(expectedResult);

      // Act
      bool result = ResultExtensions.Unwrap(value);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public void Unwrap_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result<bool, Exception> value = new(expectedError);

      // Act
      void Act() => ResultExtensions.Unwrap(value);

      // Assert
      Assert.That
         .ThrowsExactException(Act, out TestException error)
         .AreSameInstance(error, expectedError);
   }
   #endregion

   #region UnwrapAsync Result tests
   [TestMethod]
   public async Task UnwrapAsync_Result_ValueTask_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result value = new(expectedResult);

      ValueTask<Result> task = new(value);

      // Act
      bool result = await ResultExtensions.UnwrapAsync(task);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public async Task UnwrapAsync_Result_ValueTask_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result value = new(expectedError);

      ValueTask<Result> task = new(value);

      // Act
      async ValueTask Act() => await ResultExtensions.UnwrapAsync(task);

      // Assert
      TestException error = await Assert.That.ThrowsExactExceptionAsync<TestException>(Act);
      Assert.That.AreSameInstance(error, expectedError);
   }

   [TestMethod]
   public async Task UnwrapAsync_Result_Task_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result value = new(expectedResult);

      Task<Result> task = Task.FromResult(value);

      // Act
      bool result = await ResultExtensions.UnwrapAsync(task);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public async Task UnwrapAsync_Result_Task_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result value = new(expectedError);

      Task<Result> task = Task.FromResult(value);

      // Act
      async ValueTask Act() => await ResultExtensions.UnwrapAsync(task);

      // Assert
      TestException error = await Assert.That.ThrowsExactExceptionAsync<TestException>(Act);
      Assert.That.AreSameInstance(error, expectedError);
   }
   #endregion

   #region UnwrapAsync GenericValueResult tests
   [TestMethod]
   public async Task UnwrapAsync_GenericValueResult_ValueTask_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result<bool> value = new(expectedResult);

      ValueTask<Result<bool>> task = new(value);

      // Act
      bool result = await ResultExtensions.UnwrapAsync(task);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public async Task UnwrapAsync_GenericValueResult_ValueTask_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result<bool> value = new(expectedError);

      ValueTask<Result<bool>> task = new(value);

      // Act
      async ValueTask Act() => await ResultExtensions.UnwrapAsync(task);

      // Assert
      TestException error = await Assert.That.ThrowsExactExceptionAsync<TestException>(Act);
      Assert.That.AreSameInstance(error, expectedError);
   }

   [TestMethod]
   public async Task UnwrapAsync_GenericValueResult_Task_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result<bool> value = new(expectedResult);

      Task<Result<bool>> task = Task.FromResult(value);

      // Act
      bool result = await ResultExtensions.UnwrapAsync(task);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public async Task UnwrapAsync_GenericValueResult_Task_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result<bool> value = new(expectedError);

      Task<Result<bool>> task = Task.FromResult(value);

      // Act
      async ValueTask Act() => await ResultExtensions.UnwrapAsync(task);

      // Assert
      TestException error = await Assert.That.ThrowsExactExceptionAsync<TestException>(Act);
      Assert.That.AreSameInstance(error, expectedError);
   }
   #endregion

   #region UnwrapAsync GenericValueAndErrorResult tests
   [TestMethod]
   public async Task UnwrapAsync_GenericValueAndErrorResult_ValueTask_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result<bool, Exception> value = new(expectedResult);

      ValueTask<Result<bool, Exception>> task = new(value);

      // Act
      bool result = await ResultExtensions.UnwrapAsync(task);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public async Task UnwrapAsync_GenericValueAndErrorResult_ValueTask_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result<bool, Exception> value = new(expectedError);

      ValueTask<Result<bool, Exception>> task = new(value);

      // Act
      async ValueTask Act() => await ResultExtensions.UnwrapAsync(task);

      // Assert
      TestException error = await Assert.That.ThrowsExactExceptionAsync<TestException>(Act);
      Assert.That.AreSameInstance(error, expectedError);
   }

   [TestMethod]
   public async Task UnwrapAsync_GenericValueAndErrorResult_Task_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result<bool, Exception> value = new(expectedResult);

      Task<Result<bool, Exception>> task = Task.FromResult(value);

      // Act
      bool result = await ResultExtensions.UnwrapAsync(task);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public async Task UnwrapAsync_GenericValueAndErrorResult_Task_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result<bool, Exception> value = new(expectedError);

      Task<Result<bool, Exception>> task = Task.FromResult(value);

      // Act
      async ValueTask Act() => await ResultExtensions.UnwrapAsync(task);

      // Assert
      TestException error = await Assert.That.ThrowsExactExceptionAsync<TestException>(Act);
      Assert.That.AreSameInstance(error, expectedError);
   }
   #endregion
}
