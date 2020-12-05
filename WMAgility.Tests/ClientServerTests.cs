using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WMAgility2;
using Xunit;

namespace WMAgility.Tests
{
    public class ClientServerTests
    {
        [Fact]
        public async Task GetAsync_InvalidScope_ReturnsUnauthorizedResult()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var client = server.CreateClient();
            var url = "api/home";
            var expected = HttpStatusCode.Unauthorized;

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(expected, response.StatusCode);
        }
    }
}
