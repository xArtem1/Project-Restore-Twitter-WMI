﻿using BikolTwitter.Database;
using FluentValidation;

namespace BikolTwitter.Validators;

public class CreateBikolSubDtoValidator : AbstractValidator<CreateBikolSubDto>
{
	public CreateBikolSubDtoValidator(BikolTwitterDbContext dbContext)
	{
		RuleFor(c => c.TwitterUsername)
			.NotEmpty()
			.WithMessage("Twitter username must not be empty.")
			.Must(u => u is null || u.StartsWith("@"))
			.WithMessage("Twitter username must start with '@'.")
			.Must(u => !dbContext.BikolSubs.Any(s => s.TwitterUsername == u))
			.WithMessage("This twitter user already exists in database.");
	}
}
