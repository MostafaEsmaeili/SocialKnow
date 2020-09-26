﻿using FluentValidation;
using SK.Application.Common.Interfaces;

namespace SK.Application.TestValues.Commands.EditTestValue
{
    public class EditArticleCommandValidator : AbstractValidator<EditArticleCommand>
    {
        private readonly IApplicationDbContext _context;

        public EditArticleCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");
        }
    }
}
