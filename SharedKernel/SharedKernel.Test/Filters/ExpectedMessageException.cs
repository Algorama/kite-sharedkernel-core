using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharedKernel.Test.Filters
{
    public sealed class ExpectedMessageException : ExpectedExceptionBaseAttribute
    {
        private readonly Type _expectedExceptionType;
        private readonly string _expectedExceptionMessage;

        public ExpectedMessageException(Type expectedExceptionType)
        {
            _expectedExceptionType = expectedExceptionType;
            _expectedExceptionMessage = string.Empty;
        }

        public ExpectedMessageException(Type expectedExceptionType, string expectedExceptionMessage)
        {
            _expectedExceptionType = expectedExceptionType;
            _expectedExceptionMessage = expectedExceptionMessage;
        }

        protected override void Verify(Exception exception)
        {
            Assert.IsNotNull(exception);

            Assert.IsInstanceOfType(exception, _expectedExceptionType, "Wrong type of exception was thrown.");

            if (!_expectedExceptionMessage.Length.Equals(0))
            {
                Assert.AreEqual(_expectedExceptionMessage, exception.Message, "Wrong exception message was returned.");
            }
        }
    }
}