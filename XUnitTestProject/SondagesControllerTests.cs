using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject
{
    public class SondagesControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Get_ReturnBadAuthentification()
        {
            // Arrange

            // Act
            var response = await GetBadJwtAsync();

            // Assert
            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_ReturnBadApiKey()
        {
            // Arrange
            BadApiKey();
            // Act
            
            var response = await TestClient.GetAsync("api/sondages");

            // Assert
            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_ReturnAllSondagesSuccesss()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/sondages");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_ReturnOneSondagesSuccesss()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/sondages/1");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_ReturnSondagesNotFound()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/sondages/5");

            // Assert
            Assert.Equal("NotFound", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Post_ReturnSondagesForbid()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.PostAsJsonAsync("api/sondages", new
            {
                SondageId = 4,
                Reponses =  new string[] { "a", "b", "c", "d" }
            });

            // Assert
            Assert.Equal("Forbidden", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Post_ReturnSondagesOK()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.PostAsJsonAsync("api/sondages", new
            {
                SondageId = 2,
                Reponses = new string[] { "a", "b", "c", "d" }
            });

            // Assert
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Post_ReturnDoubleAnswerForbidden()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.PostAsJsonAsync("api/sondages", new
            {
                SondageId = 1,
                Reponses = new string[] { "a", "b", "c", "d" }
            });

            // Assert
            Assert.Equal("OK", response.StatusCode.ToString());

            // Act
            var response2 = await TestClient.PostAsJsonAsync("api/sondages", new
            {
                SondageId = 1,
                Reponses = new string[] { "a", "b", "c", "d" }
            });

            // Assert
            Assert.Equal("Forbidden", response2.StatusCode.ToString());
        }
    }
}
