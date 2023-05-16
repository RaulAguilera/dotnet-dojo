using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Moq;
using API.Middleware;

namespace API.Tests.Middleware
{
    public class ExceptionHandlerTests
    {
        [Fact]
        public async Task ExceptionHandler_Sould_Be_Called_For_Request()
        {
            //arrange
            var context = new DefaultHttpContext();
            context.Request.Path = "/Product";

            var nextMock = new Mock<RequestDelegate>();

            var loggerMock = new Mock<ILogger<ExceptionHandler>>();
            var middleware = new ExceptionHandler(nextMock.Object);

            // act
            await middleware.InvokeAsync(context, loggerMock.Object);

            // assert
            nextMock.Verify(n => n.Invoke(context), Times.Once);
        }

        [Fact]
        public async Task ExceptionHandler_Sould_Return_InternalServerError_On_Exception()
        {
            //arrange
            var context = new DefaultHttpContext();
            context.Request.Path = "/Product";

            var loggerMock = new Mock<ILogger<ExceptionHandler>>();
            var middleware = new ExceptionHandler(async (innerHttpContext) =>
            {
                await Task.Run(() =>
                {
                    throw new Exception();
                });
            });

            // act
            await middleware.InvokeAsync(context, loggerMock.Object);

            // assert
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }

        [Fact]
        public async Task ExceptionHandler_Sould_Log_Information_On_Exception()
        {
            //arrange
            var context = new DefaultHttpContext();
            context.Request.Path = "/products";

            var nextMock = new Mock<RequestDelegate>();

            var loggerMock = new Mock<ILogger<ExceptionHandler>>();
            var middleware = new ExceptionHandler(async (innerHttpContext) =>
            {
                await Task.Run(() =>
                {
                    throw new Exception();
                });
            });

            // act
            await middleware.InvokeAsync(context, loggerMock.Object);

            // assert
            loggerMock.Verify(x => x.LogError(
                It.IsAny<string>(),
                It.IsAny<Exception>()));
        }
    }
}

