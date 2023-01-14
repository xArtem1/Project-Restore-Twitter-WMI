using BikolTwitter.Database;
using BikolTwitter.Validators;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;

namespace BikolTwitter.Tests;

public class CreateBikolSubDtoValidatorTests
{
	private readonly CreateBikolSubDtoValidator _validator;

	public CreateBikolSubDtoValidatorTests()
	{
		var optionsBuilder = new DbContextOptionsBuilder();
		optionsBuilder.UseInMemoryDatabase("BikolTwitterDb");
		var dbContext = new BikolTwitterDbContext(optionsBuilder.Options);
		_validator = new(dbContext);
	}

	[Fact]
	public void Valdate_ForValidModel_ShouldRetrnResultWithoutErrors()
	{
        var model = new CreateBikolSubDto("@elonmusk");
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveAnyValidationErrors();
    }

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	[InlineData("xd")]
	public void Validate_ForInvalidUsername_ShouldReturnResultWithProperErrors(string userName)
	{
        var model = new CreateBikolSubDto(userName);
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(m => m.TwitterUsername);
    }
}
