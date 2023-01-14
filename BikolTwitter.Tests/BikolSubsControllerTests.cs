using BikolTwitter.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BikolTwitter.Tests;

public class BikolSubsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly WebApplicationFactory<Program> _factory;
	private readonly HttpClient _defaultClient;

    public BikolSubsControllerTests(WebApplicationFactory<Program> factory)
	{
		_factory = factory.WithWebHostBuilder(builder =>
		{
			builder.ConfigureServices(services =>
			{
				var dbContextService = services.First(s => s.ServiceType == typeof(BikolTwitterDbContext));
				services.Remove(dbContextService);
				services.AddDbContext<BikolTwitterDbContext>(options => options.UseInMemoryDatabase("BikolTwitterDb"));
			});
		});
		_defaultClient = _factory.CreateClient();
	}

	[Fact]
	public async Task Create_ForValidData_ShouldReturnCreatedStatusCode()
	{
		var model = new CreateBikolSubDto("@elonmusk");
		var response = await _defaultClient.PostAsJsonAsync("api/bikolsubs", model);
		response.StatusCode.Should().Be(HttpStatusCode.Created);
	}

	[Fact]
	public async Task Create_ForInvalidData_ShouldReturnBadRequestStatusCode()
	{
        var model = new CreateBikolSubDto(null);
        var response = await _defaultClient.PostAsJsonAsync("api/bikolsubs", model);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

	[Fact]
	public async Task GetAll_ShouldReturnOkStatuCode()
	{
		var response = await _defaultClient.GetAsync("api/bikolsubs");
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}
