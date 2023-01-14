namespace BikolTwitter.Tests;

public class BikolSubsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly WebApplicationFactory<Program> _factory;
	private readonly HttpClient _defaultClient;
    private readonly ITestOutputHelper _testOutputHelper;

    public BikolSubsControllerTests(WebApplicationFactory<Program> factory,
		ITestOutputHelper testOutputHelper)
	{
		_factory = factory.WithWebHostBuilder(builder =>
		{
			builder.ConfigureServices(services =>
			{
				var dbContextService = services.First(s => s.ServiceType == typeof(BikolTwitterDbContext));
				services.Remove(dbContextService);
				var optionsBuilder = new DbContextOptionsBuilder();
				optionsBuilder.UseInMemoryDatabase("BikolTwitterDb");
				services.AddSingleton(new BikolTwitterDbContext(optionsBuilder.Options));
			});
		});
		_defaultClient = _factory.CreateClient();
		_testOutputHelper = testOutputHelper;
	}

	[Fact]
	public async Task Create_ForValidData_ShouldReturnCreatedStatusCode()
	{
		var model = new CreateBikolSubDto("@unique");
		var response = await _defaultClient.PostAsJsonAsync("api/bikolsubs", model);
		_testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
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

	[Fact]
	public async Task Delete_ForNonexistingBikolSub_ShouldReturnNotFoundStatusCode()
	{
		var response = await _defaultClient.DeleteAsync("api/bikolsubs/0");
		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
	}

	[Fact]
	public async Task Delete_ForExistingBikolSub_ShouldReturNoContentStatusCode()
	{
		var dbContext = _factory.Services.CreateScope().ServiceProvider.GetService<BikolTwitterDbContext>();
		var testBikolSub = new BikolSub { TwitterUsername = "@test" };
		dbContext.BikolSubs.Add(testBikolSub);
		dbContext.SaveChanges();
        var response = await _defaultClient.DeleteAsync($"api/bikolsubs/{testBikolSub.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
