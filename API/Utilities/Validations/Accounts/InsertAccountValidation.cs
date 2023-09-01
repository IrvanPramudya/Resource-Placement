﻿using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class InsertAccountValidation:AbstractValidator<NewAccountDto>
    {
        public InsertAccountValidation()
        {
            RuleFor(account => account.Password)
                .NotEmpty().WithMessage("Password can not be Null")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
                .WithMessage("Password Must Contain 1 Lower Case, 1 Upper Case, 1 Number, and Minimal 8 Characters");
        }
    }
}
