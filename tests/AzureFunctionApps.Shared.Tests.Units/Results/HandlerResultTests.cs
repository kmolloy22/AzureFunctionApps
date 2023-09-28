using AzureFunctionApps.Shared.Kernel;
using AzureFunctionApps.Shared.Kernel.Errors;
using AzureFunctionApps.Shared.Kernel.Results;
using Moq;

namespace AzureFunctionApps.Shared.Tests.Units.Results
{
    public class HandlerResultTests
    {
        [Fact]
        public void IsSuccess_WithoutError_ReturnsTrue()
        {
            var handlerResult = new HandlerResult(new object());

            var result = handlerResult.IsSuccess;

            Assert.True(result);
        }
        
        [Fact]
        public void IsSuccess_WithError_ReturnsFalse()
        {
            var errorMock = new Mock<HandlerError>();
            var handlerResult = new HandlerResult(errorMock.Object);

            var result = handlerResult.IsSuccess;

            Assert.False(result);
        }

        [Fact]
        public void IsModelOfType_WithMatchingType_ReturnsTrue()
        {
            var handlerResult = new HandlerResult("test");

            var result = handlerResult.IsModelOfType<string>();

            Assert.True(result);
        }

        [Fact]
        public void IsModelOfType_WithNonMatchingType_ReturnsFalse()
        {
            var handlerResult = new HandlerResult("test");

            var result = handlerResult.IsModelOfType<int>();

            Assert.False(result);
        }

        [Fact]
        public void Model_WithNonNullModel_ReturnsModel()
        {
            var handlerResult = new HandlerResult("test");

            var result = handlerResult.Model();

            Assert.Equal("test", result);
        }

        //[Fact]
        //public void Model_WithNullModel_ThrowsInvalidOperationException()
        //{
        //    var handlerResult = new HandlerResult();

        //    Assert.Throws<InvalidOperationException>(() => handlerResult.Model<Empty>());
        //}

        [Fact]
        public void Model_WithNonMatchingType_ThrowsInvalidCastException()
        {
            var handlerResult = new HandlerResult("test");

            Assert.Throws<InvalidCastException>(() => handlerResult.Model<int>());
        }

        [Fact]
        public void Model_WithMatchingType_ReturnsModel()
        {
            var handlerResult = new HandlerResult("test");

            var result = handlerResult.Model<string>();

            Assert.Equal("test", result);
        }

        [Fact]
        public void HasModel_WithNonNullModel_ReturnsTrue()
        {
            var handlerResult = new HandlerResult("test");

            var result = handlerResult.HasModel;

            Assert.True(result);
        }

        [Fact]
        public void HasModel_WithNullModel_ReturnsFalse()
        {
            var handlerResult = new HandlerResult();

            var result = handlerResult.HasModel;

            Assert.False(result);
        }

        [Fact]
        public void IsEmpty_WithEmptyModel_ReturnsTrue()
        {
            var handlerResult = new HandlerResult(Empty.Value);

            var result = handlerResult.IsEmpty;

            Assert.True(result);
        }

        [Fact]
        public void IsEmpty_WithNonNullModel_ReturnsFalse()
        {
            var handlerResult = new HandlerResult("test");

            var result = handlerResult.IsEmpty;

            Assert.False(result);
        }

        [Fact]
        public void HasError_WithNonNullError_ReturnsTrue()
        {
            var errorMock = new Mock<HandlerError>();
            var handlerResult = new HandlerResult(errorMock.Object);

            var result = handlerResult.HasError;

            Assert.True(result);
        }

        [Fact]
        public void HasError_WithNullError_ReturnsFalse()
        {
            var handlerResult = new HandlerResult(new object());

            var result = handlerResult.HasError;

            Assert.False(result);
        }

    }
}