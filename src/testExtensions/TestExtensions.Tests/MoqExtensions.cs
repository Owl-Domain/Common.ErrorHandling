namespace TestExtensions.Tests;

internal static class MoqExtensions
{
   #region Methods
   public static void VerifyFailFormat(this Mock<IAssert> mock, Times times)
   {
      mock.Verify(
         a => a.Fail(
            It.IsAny<string>(),
            It.IsAny<object?[]>()),
         times);
   }
   public static void VerifyFailFormatOnly(this Mock<IAssert> mock, Times times)
   {
      VerifyFailFormat(mock, times);
      mock.VerifyNoOtherCalls();
   }
   #endregion
}
