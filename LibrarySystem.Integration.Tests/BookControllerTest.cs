using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using LibrarySystem.Shared.Wrappers;
using LibrarySystem.Shared.DTOs;

namespace LibrarySystem.Integration.Tests;

public class BookControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BookControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPaginatedBooks_ShouldReturn200AndValidData()
    {
        // Act
        var response = await _client.GetAsync("/api/books/GetBooks?page=1&pageSize=2");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<ResponseResult<PaginatedResult<BookDto>>>>();

        result.Should().NotBeNull();
        result!.IsSucceed.Should().BeTrue();
        result.Model.Should().NotBeNull();
        result.Model.Model.Should().NotBeNull();
        result.Model!.Model.Items.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GetPaginatedBooks_ShouldReturnBadRequest_WhenInvalidParameters()
    {
        // Act
        var response = await _client.GetAsync("/api/books/GetBooks?page=-1&pageSize=0");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    public class ApiResponse<T>
    {
        public bool IsSucceed { get; set; }
        public T Model { get; set; }
        public string Message { get; set; }
        public string Ex { get; set; }
    }
}
