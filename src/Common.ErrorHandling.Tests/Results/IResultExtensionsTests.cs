using System.Threading.Tasks;
using OwlDomain.Common.Results;

namespace OwlDomain.Common.ErrorHandling.Tests.Results;

[TestClass]
public sealed class IResultExtensionsTests
{
   #region Unwrap tests
   [TestMethod]
   public void Unwrap_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result value = new(expectedResult);

      // Act
      bool result = IResultExtensions.Unwrap(value);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public void Unwrap_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result value = new(expectedError);

      // Act
      void Act() => IResultExtensions.Unwrap(value);

      // Assert
      Assert.That
         .ThrowsExactException(Act, out TestException error)
         .AreSameInstance(error, expectedError);
   }
   #endregion

   #region UnwrapAsync tests
   [TestMethod]
   public async Task UnwrapAsync_ValueTask_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result value = new(expectedResult);

      ValueTask<IResult<bool, Exception>> task = new(value);

      // Act
      bool result = await IResultExtensions.UnwrapAsync(task);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public async Task UnwrapAsync_ValueTask_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result value = new(expectedError);

      ValueTask<IResult<bool, Exception>> task = new(value);

      // Act
      async ValueTask Act() => await IResultExtensions.UnwrapAsync(task);

      // Assert
      TestException error = await Assert.That.ThrowsExactExceptionAsync<TestException>(Act);
      Assert.That.AreSameInstance(error, expectedError);
   }

   [TestMethod]
   public async Task UnwrapAsync_Task_WithValue_ReturnsValue()
   {
      // Arrange
      const bool expectedResult = true;
      Result value = new(expectedResult);

      Task<IResult<bool, Exception>> task = Task.FromResult<IResult<bool, Exception>>(value);

      // Act
      bool result = await IResultExtensions.UnwrapAsync(task);

      // Assert
      Assert.That.AreEqual(result, expectedResult);
   }

   [TestMethod]
   public async Task UnwrapAsync_Task_WithError_ThrowsError()
   {
      // Arrange
      TestException expectedError = new();
      Result value = new(expectedError);

      Task<IResult<bool, Exception>> task = Task.FromResult<IResult<bool, Exception>>(value);

      // Act
      async ValueTask Act() => await IResultExtensions.UnwrapAsync(task);

      // Assert
      TestException error = await Assert.That.ThrowsExactExceptionAsync<TestException>(Act);
      Assert.That.AreSameInstance(error, expectedError);
   }
   #endregion
}
